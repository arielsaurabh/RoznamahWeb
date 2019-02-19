import {module} from 'angular';

import {AuthenticationInterceptor} from './account-interceptor.factory';
import AccountService from './account.service';
import CookieService from './Cookie.Service';
import {LoginModule} from './login/login.module';
import { RegisterModule } from './register/register.module';

export const AccountModule = module('app.components.accounts', [LoginModule, RegisterModule])
    .service('AccountService', AccountService)
    .service('AuthTokenInterceptor', AuthenticationInterceptor)
    .service('CookieService', CookieService)   
    .name;