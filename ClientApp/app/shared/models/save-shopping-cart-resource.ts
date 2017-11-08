import { CartItemResource } from './cart-item-resource';
export interface ShoppingCartResource
{
    id: number;
    cartItems: CartItemResource[];
    totalPrice: number;
}