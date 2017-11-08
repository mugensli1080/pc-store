import { NgModule } from '@angular/core';
import { BrowserXhr } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AdminModule } from './admin/admin.module';
import { ClientModule } from './client/client.module';
import { CoreModule } from './core/core.module';
import { BrowserXhrWithProgress } from './shared/services/progress.service';
import { SharedModule } from './shared/shared.module';


@NgModule({
    declarations: [
    ],
    imports: [
        AdminModule,
        ClientModule,
        CoreModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'products', pathMatch: 'full' },
            { path: '**', redirectTo: 'home' }
        ]),
        SharedModule,
    ],
    providers: [
        { provide: BrowserXhr, useClass: BrowserXhrWithProgress }
    ]
})
export class AppModuleShared
{
}
