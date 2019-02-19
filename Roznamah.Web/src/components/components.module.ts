import {module} from 'angular';
import { AccountModule } from './accounts/account.module';

export const ComponentsModule = module('app.components', [
    AccountModule,
    ])
    .name;