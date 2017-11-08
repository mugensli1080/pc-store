import { ShoppingCartResource } from '../../../shared/models/save-shopping-cart-resource';
import { Subscription } from 'rxjs/Rx';
import { CartItemResource } from '../../../shared/models/cart-item-resource';
import { ShoppingCartService } from '../../../shared/services/shopping-cart.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'shopping-cart',
    templateUrl: './shopping-cart.component.html',
    styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit
{
    cartItems: ShoppingCartResource;
    cartItemsSubscription: Subscription;
    constructor(private _shoppingCartService: ShoppingCartService) { }

    ngOnInit()
    {
        this.cartItemsSubscription = this._shoppingCartService
            .trackCart()
            .subscribe(c =>
            {
                this.cartItems = c;
                console.log("cart: ", this.cartItems);
            });
    }

}
