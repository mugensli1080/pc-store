import { Subscription } from 'rxjs/Rx';
import { ShoppingCartResource } from '../../../shared/models/save-shopping-cart-resource';
import { ShoppingCartService } from '../../../shared/services/shopping-cart.service';
import { Auth0Service } from '../../../shared/services/auth0/auth0.service';
import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit, OnDestroy
{
    itemsCount: number = 0;
    shoppingCartSubscription: Subscription;
    constructor(public auth0: Auth0Service, private _shoppingCartService: ShoppingCartService) { }

    ngOnInit(): void
    {
        this.shoppingCartSubscription = this._shoppingCartService
            .trackCart()
            .subscribe(cart =>
            {
                this.itemsCount = cart.cartItems.length > 0 ? cart.cartItems.length : 0;
                console.log("C: ", cart);
            });
        // let cart = localStorage.getItem('shopping-cart');
        // if (!cart) this.itemsCount = 0;
        // this.itemsCount = (<ShoppingCartResource>JSON.parse(cart!)).cartItems.length + 1;

    }

    ngOnDestroy()
    {
        if (this.shoppingCartSubscription) this.shoppingCartSubscription.unsubscribe();
    }
}
