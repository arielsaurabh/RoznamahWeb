import IAccountService from '../account.service';
//import IConfigService from '../../../config.service';
// import IFacebookService from '../../story/facebook.service';
// import ICountryService from '../../countries/country.service';

declare var $: JQueryStatic;
export interface IRegisterComponentBindings {
    isRegistrationFormVisible: boolean;
}
export interface IRegisterComponentController extends IRegisterComponentBindings { }

export class RegisterController implements IRegisterComponentController {

    static $inject: Array<string> = ['AccountService', '$q', '$rootScope', '$window', '$sce', '$scope'];
    public isRegistrationFormVisible = false;
    public userExits = false;
    public registrationModel = {
        Email: "",
        PhoneNumber: "",
        Avtar: "",
        Name: "",
        DateOfBirth: "",
        Gender: "",
        RegistrationType: "",
        IdToken: "",
        SocialId: "",
        Token: "",
        Password: "",
        ConfirmPassword: ""
    };
    public loginModel = {
        username: "",
        password: "",
        grant_type: "password"
      };

    constructor(public AccountService: IAccountService, public $q: any, public $rootScope, public $window, public $sce: any, public $scope) {
        var self = this;
        this.$rootScope.$on('event:social-sign-in-success', function (event, userDetails, $scope) {
            // self.isRegistrationFormVisible = true;
            self.registrationModel.Name = userDetails.name;
            self.registrationModel.Email = userDetails.email;
            self.registrationModel.Avtar = userDetails.imageUrl;
            self.registrationModel.RegistrationType = userDetails.provider;
            self.registrationModel.IdToken = userDetails.idToken;
            self.registrationModel.SocialId = userDetails.uid;
            self.registrationModel.Token = userDetails.token;
            self.registrationModel.Password = userDetails.uid;
            self.registrationModel.ConfirmPassword = userDetails.uid;
            self.UserExits(userDetails.email);
        });
    }

    public $onInit(): void {

    }

    public registerUser() {
        //debugger;
        this.AccountService.registerUser(this.registrationModel).then(result => {
            console.log(result);
            window.location.href = "/sign-in/";
        });
    }

    public UserExits(email) {
        var self = this;
        this.AccountService.userExits(email).then(result => {
            if(result.data){
                self.userExits = true;
                this.loginModel.username = result.data.Email;
                this.loginModel.password = result.data.SocialId;
            }
            else{
                self.isRegistrationFormVisible = true;
                self.$scope.$apply();
            }
        });
    }

    public login(){
        var self = this;
        this.AccountService.loginUser(this.loginModel).then((result)=>{
            console.log(result);
        })
    }

    public twitterLogin(){
       // debugger;
        var self = this;
        this.$window.twttr.connect(self.callback)
       // window.location.href = "http://twitter.com";
    }

    public callback(result){
        console.log(result);
    }
}

export default class RegisterComponent implements ng.IComponentOptions {
    public bindings: any;
    public controller: any;
    public template: string;
    public templateUrl: string;
    public transclude: boolean;

    constructor() {
        this.bindings = {

        };

        this.controller = RegisterController;
        this.templateUrl = "src/components/accounts/register/register.component.html";
    }
}
