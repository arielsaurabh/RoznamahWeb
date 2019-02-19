import IConfigService from '../../config.service';

export interface ICookieService {
    getValue(name: string): any;
    setValue(name:string, values:any): void;
}

export default class CookieService implements ICookieService {

    public httpService: ng.IHttpService;

    static $inject: Array<string> = ['ConfigService'];
    constructor(public ConfigService: IConfigService) {
        
    }

    //get a cookie by name
    public getValue(name: string):any{
        var gCookieVal = document.cookie.split("; ");
           for (var i = 0; i < gCookieVal.length; i++) {
               // a name/value pair (a crumb) is separated by an equal sign
               var gCrumb = gCookieVal[i].split("=");
               if (name === gCrumb[0]) {
                   var value = '';
                   try {
                       //decode cookies value
                       value = JSON.parse(window.atob(gCrumb[1]));
                   } catch (e) {
                       value = unescape(window.atob(gCrumb[1]));
                   }
                   return value;
               }
           }
           // a cookie with the requested name does not exist
           return null;
        
    }

    public setValue(name:string, values:any){
        var cookie = name + '=';
        if (typeof values === 'object') {
            var expires = '';
            //encode the cookies value
            cookie += (typeof values.value === 'object') ? window.btoa(JSON.stringify(values.value)) + ';' : window.btoa(values.value) + ';';
            if (values.expires) {
                var date = new Date();
                date.setTime(date.getTime() + (values.expires * 24 * 60 * 60 * 1000));
                expires = date.toUTCString();
            }
            cookie += (!values.session) ? 'expires=' + expires + ';' : '';
            cookie += (values.path) ? 'path=' + values.path + ';' : '';
            cookie += (values.secure) ? 'secure;' : '';
        } else {
            //encode the cookies value
            cookie += (window.btoa(values) + ';');
        }
        document.cookie = cookie;
    }
}