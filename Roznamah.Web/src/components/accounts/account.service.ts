//import Payment from './payment.model';
import IConfigService from '../../config.service';

export interface IAccountService {
    registerUser(userDetails): ng.IHttpPromise<any>;
    loginUser(userDetails): ng.IHttpPromise<any>;
}

export default class AccountService implements IAccountService {

    public httpService: ng.IHttpService;

    static $inject: Array<string> = ['$http', 'ConfigService'];
    constructor($http: ng.IHttpService, public ConfigService: IConfigService) {
        this.httpService = $http;
    }

    public registerUser(userDetails:any): ng.IHttpPromise<any> {
        var payload = userDetails;
        var url = this.ConfigService.getApiUrl() + "api/Account/Register";
        return this.httpService.post(url, payload, {});
    }

    public loginUser(userDetails:any): ng.IHttpPromise<any> {
        var payload = 'grant_type=password&username='+userDetails.username+'&password='+userDetails.password;
        var url = this.ConfigService.getApiUrl() + "Token";
        return this.httpService.post(url, payload, {});
    }

    public userExits(email:any): ng.IHttpPromise<any> {
        var url = this.ConfigService.getApiUrl() + "api/Account/UserExits?email="+email;
        return this.httpService.post(url, {});
    }
}