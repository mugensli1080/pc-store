import { SaveShoppingCartResource } from '../models/shopping-cart-resource';
import { Observable, Subject } from 'rxjs/Rx';
import { ShoppingCartResource } from '../models/save-shopping-cart-resource';
import { SaveShoppingCart } from '../models/save-shopping-cart';
import { CartItemResource } from '../models/cart-item-resource';
import { Injectable } from '@angular/core';


@Injectable()
export class ShoppingCartService
{
    private cart: Subject<ShoppingCartResource>;
    constructor()
    {
        this.cart = new Subject<ShoppingCartResource>();
        this.setCart();
    }

    addItem(cartItem: CartItemResource)
    {
        let localCart: ShoppingCartResource = JSON.parse(localStorage.getItem('shopping-cart')!);
        if (localCart.cartItems.length < 1)
        {
            localCart.cartItems.push(cartItem);
            this.cartChanges(localCart);
        }
    }

    deleteLocalCartItem(itemId: number)
    {
        // let cart = <ShoppingCartResource>this.getCart();
        // let item = cart.cartItems.filter(i => i.id === itemId)[0];
        // if (item)
        //     cart.cartItems = cart.cartItems.filter(i => i.id !== itemId);

        // localStorage.setItem('shopping-cart', JSON.stringify(cart));
    }

    private setCart()
    {
        let localCart: ShoppingCartResource = JSON.parse(localStorage.getItem('shopping-cart')!);
        if (localCart) this.cart.next(localCart);
        else
        {
            localStorage.setItem('shopping-cart', JSON.stringify({ id: 5000, cartItems: <CartItemResource[]>[] }));
            localCart = JSON.parse(localStorage.getItem('shopping-cart')!);
            this.cartChanges(localCart);
        }

    }

    cartChanges(cart: ShoppingCartResource)
    {
        if (this.cart)
        {
            localStorage.setItem('shopping-cart', JSON.stringify(cart));
            return this.cart.next(cart);
        };
    }

    trackCart() { return this.cart; }
}