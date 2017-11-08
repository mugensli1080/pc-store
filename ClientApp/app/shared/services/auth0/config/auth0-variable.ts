interface AuthConfig
{
    clientID: string;
    domain: string;
    callbackURL: string;
}

export const AUTH_CONFIG: AuthConfig = {
    clientID: 'vQcQvrsLX2pnLvODmrOE2619xBG9H4R9',
    domain: 'lock22.au.auth0.com',
    callbackURL: 'http://localhost:5000/products'
}