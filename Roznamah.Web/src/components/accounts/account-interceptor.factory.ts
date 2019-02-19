// import IConfigService from '../../config.service';

export class AuthenticationInterceptor {

    public static Factory($q: ng.IQService, localStorageService: angular.local.storage.ILocalStorageService, $window: any) {
        return new AuthenticationInterceptor($q, localStorageService, $window);
    }

    static $inject: Array<string> = ['$q', 'localStorageService', '$window'];
    constructor(public $q: ng.IQService, public localStorageService: angular.local.storage.ILocalStorageService, public $window: any) {

    }

    public request = (config) => {
        console.log(config.url);
        var token = this.localStorageService.get('token');

        config.headers = config.headers || {};
        if (token)
            config.headers.Authorization = 'Bearer ' + token;

        var regex = /v3.exchangerate-api.com/i;
        if (regex.test(config.url))
            //Detach the header
            delete config.headers.Authorization;
        debugger;
        if(config.url.indexOf("Token") != -1){
            debugger;
            config.headers["Content-Type"] = "application/x-www-form-urlencoded";
        }

        return config;
    };

    //Method name should be exactly "response" - http://docs.angularjs.org/api/ng/service/$http
    public response(response) {

        return response || this.$q.when(response);
    }

    public responseError(rejection) {
        //var authToken = rejection.config.headers.Authorization;
        console.log(rejection.status);
        if (rejection.status === 401) {
            var authenticationUrl = "/user-login/"; // todo: data drive this
            console.log('rejected');
            window.localStorage.removeItem('roznamahApp.token');
            window.location.href = authenticationUrl;
        }
        // Otherwise, default behavior
        //return this.service.$q.reject(rejection);
    }
}