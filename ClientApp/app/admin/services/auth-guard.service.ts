import { Auth0Service } from '../../shared/services/auth0/auth0.service';
import { Observable } from 'rxjs/Rx';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { Injectable, state } from '@angular/core';

@Injectable()
export class AuthGuardService implements CanActivate
{
    constructor(private _auth: Auth0Service) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean>
    {
        if (this._auth.isAuthenticated()) return true;

        this._auth.login();
        return false;
    }


}