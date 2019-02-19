import {module} from 'angular';

import RegisterComponent from './register.component';

export const RegisterModule = module('app.components.accounts.register', [])        
    .component('register', new RegisterComponent())  
    .name;