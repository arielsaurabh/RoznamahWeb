'use strict';

import { module } from 'angular';
import ConfigService from './config.service';
import { ComponentsModule } from './components/components.module';

var bootstrap = require('angular-ui-bootstrap');
var animate = require('angular-animate');
var storage = require('angular-local-storage');
var tinymce = require('angular-ui-tinymce');
var fileUpload = require('angular-file-upload');
var clipboard = require('ngclipboard');
var ngSanitize = require('angular-sanitize');
var ngMaterial = require('angular-material')
var socialLogin = require('angularjs-social-login');
var twitterLogin=require('twitter');
export let roznamahApp = module('roznamahApp',
    [ComponentsModule,
        'ui.bootstrap',
        'ngAnimate',
        'ngclipboard',
        'LocalStorageModule',
        'ui.tinymce',
        'angularFileUpload',
        'ngSanitize',
        'ngMaterial',
        'socialLogin'
        //'twitterLogin'
    ])
.service('ConfigService', ConfigService);

roznamahApp.config(function (
    $httpProvider,
    $windowProvider,
    localStorageServiceProvider: angular.local.storage.ILocalStorageServiceProvider,
    socialProvider
) {

    $httpProvider.interceptors.push('AuthTokenInterceptor');

    //social login start
    socialProvider.setGoogleKey("972942847866-v0e6hnqtmaikkvkbb0mbf480crnmo2ld.apps.googleusercontent.com")
  //  socialProvider.setGoogleKey("139842795758-8d98j1quikqbtuejr4skuj7ugs6tbc8g.apps.googleusercontent.com");
    //socialProvider.setLinkedInKey("YOUR LINKEDIN CLIENT ID");
    // socialProvider.setFbKey({appId: "2201181426667350", apiVersion: "v3.2"}); //server
    socialProvider.setFbKey({appId: "1151737674989121", apiVersion: "v3.2"}); //local
var client = new twitterLogin({
  consumer_key: 'gB2Zdy8PtEqgIZdd11MWkv9Bd',
  consumer_secret:'L0IAHgu7TOvjgJnQCLwcUOjXYO6HDDo9M3LsYisAMRzZsvHqBj',
  access_token_key:'2164247124-VeWnpivmpIGEbaTcP28EOw1hwErfTKuRfPw62dC',
  access_token_secret: 'F30RxjccUApyOFoyehSo7X7jkJRLMl9fHma1Y1jjg9trQ '
}); 
  
    //social login end

    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
    var window = $windowProvider.$get();
    // var config = window.domondoAppConfig;

    // localStorageServiceProvider.setPrefix("domondoApp");
    // localStorageServiceProvider.setStorageType('localStorage');    
    // localStorageServiceProvider.setStorageCookieDomain(config.WebsiteUrl);        
});

roznamahApp.directive('compile', ['$compile', function ($compile) {
    return function (scope, element, attrs) {
        scope.$watch(
            function (scope) {
                // watch the 'compile' expression for changes
                return scope.$eval(attrs.compile);
            },
            function (value) {
                // when the 'compile' expression changes
                // assign it into the current DOM
                element.html(value);

                // compile the new DOM and link it to the current
                // scope.
                // NOTE: we only compile .childNodes so that
                // we don't get into infinite loop compiling ourselves
                $compile(element.contents())(scope);
            }
        );
    };
}])
