// Define gulp before we start
var gulp = require('gulp');
var gulpTsConfig = require('gulp-ts-config');
// Define Sass and the autoprefixer
var sass = require('gulp-sass');

var ts = require('gulp-typescript');
var tsProject = ts.createProject("tsconfig.json");

var browserify = require("browserify");
var source = require('vinyl-source-stream');
var tsify = require("tsify");
var buffer = require('vinyl-buffer');

var sourcemaps = require('gulp-sourcemaps');
var uglify = require("gulp-uglify");
var minify = require("gulp-minify");

var util = require('gulp-util');

var currentEnvironment = (util.env.production) ? "Production" : null;
currentEnvironment = (currentEnvironment == null) ? ((util.env.staging) ? "Staging" : null) : currentEnvironment;
currentEnvironment = (currentEnvironment == null) ? ((util.env.development) ? "Development" : null) : currentEnvironment;
currentEnvironment = (currentEnvironment == null) ? "Local" : currentEnvironment;

// This is an object which defines paths for the styles.
// Can add paths for javascript or images for example
// The folder, files to look for and destination are all required for sass
var paths = {
    styles: {
        src: './src/sass',
        files: './src/sass/**/*.scss',
        dest1: './Roznamah.Web/css',
        dest2: './src/css'
    },
    typescript: {
      src: './Roznamah.Web/src',
      files: './Roznamah.Web/src/**/*.ts',
      dest: './Roznamah.Web/js'
    },
    config: {
      file: './Roznamah.Web/src/appsettings.json'
    }
}

// A display error function, to format and make custom errors more uniform
// Could be combined with gulp-util or npm colors for nicer output
var displayError = function (error) {
    // Initial building up of the error
    var errorString = '[' + error.plugin + ']';
    errorString += ' ' + error.message.replace("\n", ''); // Removes new line at the end
    // If the error contains the filename or line number add it to the string
    if (error.fileName)
        errorString += ' in ' + error.fileName;
    if (error.lineNumber)
        errorString += ' on line ' + error.lineNumber;
    // This will output an error like the following:
    // [gulp-sass] error message in file_name on line 1
    console.error(errorString);
}

gulp.task('sass', function () {
    // Taking the path from the above object
    gulp.src(paths.styles.files)
    // Sass options - make the output compressed and add the source map
    // Also pull the include path from the paths object
    .pipe(sass({
        outputStyle: 'compressed',
        sourceComments: 'map',
        includePaths: [paths.styles.src]
    }))
    // If there is an error, don't stop compiling but use the custom displayError function
    .on('error', function (err) {
        displayError(err);
    })
    // Funally put the compiled sass into a css file
    .pipe(gulp.dest(paths.styles.dest1))
    .pipe(gulp.dest(paths.styles.dest2))
});

gulp.task("dev", function () {
    return browserify({
        basedir: '.',
        debug: true,
        entries: ['Roznamah.Web/src/app.ts'],
        cache: {},
        packageCache: {}
    })
    .plugin(tsify, {
                noImplicitAny: false,
                target: 'ES5',
                noExternalResolve: false,
                module: 'commonjs',
                removeComments: true
            })
    .on('error', console.error.bind(console))
    .bundle()
    .on('error', console.error.bind(console))
    .pipe(source('app.js'))
    .pipe(buffer())
    //.pipe(minify())
    //.pipe(uglify({ mangle: false }))
    .pipe(sourcemaps.init({ loadMaps: true }))    
    .pipe(sourcemaps.write('./'))
    //.pipe(gulp.dest("src/js"))
    .pipe(gulp.dest(paths.typescript.dest));
});

/*
gulp.task('config', function () {
  gulp.src(paths.config.file)
  .pipe(gulpTsConfig('AppSettings', { environment: currentEnvironment }))
  .pipe(gulp.dest('./Roznamah.Web/src'))
});*/

// This is the default task - which is run when `gulp` is run
// The tasks passed in as an array are run before the tasks within the function
gulp.task('default', ['sass', 'dev'], function () {
    // Watch the files in the paths object, and when there is a change, fun the functions in the array
    gulp.watch(paths.typescript.files, ['dev'])
    // Also when there is a change, display what file was changed, only showing the path after the 'sass folder'
    .on('change', function (evt) {
        console.log(
            '[watcher] File ' + evt.path.replace(/.*(?=ts)/, '') + ' was ' + evt.type + ', compiling...'
        );
    });

    gulp.watch(paths.styles.files, ['sass'])
    // Also when there is a change, display what file was changed, only showing the path after the 'sass folder'
    .on('change', function (evt) {
        console.log(
            '[watcher] File ' + evt.path.replace(/.*(?=sass)/, '') + ' was ' + evt.type + ', compiling...'
        );
    });

    gulp.watch(paths.config.file, ['config'])
    // Also when there is a change, display what file was changed, only showing the path after the 'sass folder'
    .on('change', function (evt) {
        console.log(
            '[watcher] File ' + evt.path.replace(/.*(?=sass)/, '') + ' was ' + evt.type + ', compiling...'
        );
    });
});
