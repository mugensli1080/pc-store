import { UserProfile } from './config/user-profile';
import { Router } from '@angular/router';
import { AUTH_CONFIG } from './config/auth0-variable';
import { Injectable } from '@angular/core';
import { JwtHelper } from 'angular2-jwt';
import * as auth0 from 'auth0-js';

@Injectable()
export class Auth0Service
{
    userProfile: auth0.Auth0UserProfile;
    roles: any[];
    auth0 = new auth0.WebAuth({
        clientID: AUTH_CONFIG.clientID,
        domain: AUTH_CONFIG.domain,
        responseType: 'token id_token',
        // audience: `https://${AUTH_CONFIG.domain}/userinfo`,
        // redirectUri: 'http://localhost:5000/products',
        scope: 'openid email profile'
    });

    constructor(private _router: Router)
    {
        let userProfile = localStorage.getItem('profile');
        if (userProfile) this.userProfile = <auth0.Auth0UserProfile>JSON.parse(userProfile);
    }

    login()
    {
        this.auth0.authorize();
    }

    handleAuthentication()
    {
        this.auth0.parseHash((err, authResult) =>
        {
            console.log(authResult);
            if (authResult && authResult.accessToken && authResult.idToken)
            {
                window.location.hash = '';
                this.setSession(authResult);
                this._router.navigate(['/products']);
            } else if (err)
            {
                this._router.navigate(['/products']);
                console.log(err);
                alert(`Error: ${err.error}. Check the console for further detail.`);
            }
        })
    }

    private setSession(authResult: auth0.Auth0DecodedHash)
    {
        let expire = (authResult.expiresIn! * 1000) + new Date().getTime();
        const expireAt = JSON.stringify(expire);

        localStorage.setItem('access_token', authResult.accessToken!);
        localStorage.setItem('id_token', authResult.idToken!);
        localStorage.setItem('expire_at', expireAt);

        this.setRole();

        this.auth0.client.userInfo(
            authResult.accessToken!,
            (err, user) =>
            {
                if (err) throw err;
                else
                {
                    localStorage.setItem('profile', JSON.stringify(user));
                    this.userProfile = user;
                    console.log(user)
                }
            });

    }

    setRole()
    {
        let jwtHelper = new JwtHelper();
        let idToken = localStorage.getItem('id_token');
        if (idToken)
        {
            console.log(idToken);
            let decodedToken = jwtHelper.decodeToken(idToken);
            this.roles = decodedToken['https://pc_store.com/roles'];
            console.log(this.roles);
        }
    }

    isRole(roleName: string)
    {
        if (this.roles)
            return this.roles.indexOf(roleName) > -1;

        return false;
    }

    private logout()
    {
        localStorage.removeItem('access_token');
        localStorage.removeItem('id_token');
        localStorage.removeItem('expire_at');
        localStorage.removeItem('profile');

        this._router.navigate(['/products']);
    }

    isAuthenticated()
    {
        const expireAt = JSON.parse(localStorage.getItem('expire_at')!);
        return new Date().getTime() < expireAt;
    }

}