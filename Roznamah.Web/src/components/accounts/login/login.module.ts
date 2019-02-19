import {module} from 'angular';

import LoginComponent from './login.component';

export const LoginModule = module('app.components.accounts.login', [])        
    .component('login', new LoginComponent())  
    .name;