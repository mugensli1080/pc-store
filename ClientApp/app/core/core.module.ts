import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        NavMenuComponent
    ],
    imports: [
        SharedModule
    ],
    exports: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        NavMenuComponent
    ],
    providers: [
        // TODO: and services
        // DemoService
    ]
})
export class CoreModule { }
