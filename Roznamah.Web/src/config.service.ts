export interface IConfigService {
    // getWebsiteUrl(): string;
    getApiUrl(): string;
}

export default class ConfigService implements IConfigService {

    public httpService: ng.IHttpService;   
    private websiteUrl: string; 
    private apiUrl: string;

    static $inject: Array<string> = ['$http', '$window','$sce'];
    constructor($http: ng.IHttpService, public $window:any) {
        this.httpService = $http;
        //this.websiteUrl = $window.domondoAppConfig.WebsiteUrl;
        //this.apiUrl = $window.domondoAppConfig.DomondoApi;
    }

    // public getWebsiteUrl(): string {
    //     return this.websiteUrl;
    // }
    public getApiUrl(): string {
            return "http://localhost:50545/";
    }
}
