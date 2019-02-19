import IAccountService from '../account.service';
import ICookieService from '../Cookie.Service';
// import IConfigService from '../../../config.service';
// import IFacebookService from '../../story/facebook.service';
// import ICountryService from '../../countries/country.service';

declare var $: JQueryStatic;
export interface ILoginComponentBindings {

}
export interface ILoginComponentController extends ILoginComponentBindings { }

export class LoginController implements ILoginComponentController {

    public loginModel = {
        username: "",
        password: "",
        grant_type: "password"
      };

    static $inject: Array<string> = ['AccountService','$q', '$rootScope', 'CookieService'];
    constructor(public AccountService: IAccountService, public $q: any, public $rootScope, public CookieService: ICookieService) {
        var self = this;
        this.$rootScope.$on('event:social-sign-in-success', function(event, userDetails){
            self.loginModel.username = userDetails.email;
            self.loginModel.password = userDetails.uid;
            self.AccountService.loginUser(self.loginModel).then(result=>{
                console.log("Login sucess");
            });
        })
    }

    public $onInit(): void {
        // var userName = this.CookieService.getValue("zxz12a");
        // var password = this.CookieService.getValue("zxy12a");
        // if(userName && password){
        //     this.loginModel.username = userName;
        //     this.loginModel.password = password;
        // }
    }

    // public rememberMe():void{
    //     this.CookieService.setValue('zxz12a', this.loginModel.username);
    //     this.CookieService.setValue('zxy12a', this.loginModel.password);
    // }

}

export default class LoginComponent implements ng.IComponentOptions {
    public bindings: any;
    public controller: any;
    public template: string;
    public templateUrl: string;
    public transclude: boolean;

    constructor() {
        this.bindings = {
            
        };

        this.controller = LoginController;
        this.templateUrl = "src/components/accounts/login/login.component.html";
    }
}
