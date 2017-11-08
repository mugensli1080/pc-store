import { SaveProductResource } from "./save-product-resource";

export interface SaveShoppingCart
{
    id: number;
    products: SaveProductResource[];
}