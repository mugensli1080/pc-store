import { Auth0Service } from '../../../shared/services/auth0/auth0.service';
import { Component } from '@angular/core';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent
{
    constructor(public auth: Auth0Service)
    {
        auth.handleAuthentication();
    }
}
