import { CartItemResource } from './cart-item-resource';
export interface SaveShoppingCartResource
{
    id: number;
    cartItems: CartItemResource[];
}