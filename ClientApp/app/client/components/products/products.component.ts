import { ShoppingCartService } from '../../../shared/services/shopping-cart.service';
import { CartItemResource } from '../../../shared/models/cart-item-resource';
import { ActivatedRoute } from '@angular/router';
import { Category } from '../../../shared/models/category';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Rx';

import { UploadFolderPath } from '../../../shared/config/path';
import { ProductResource } from '../../../shared/models/product-resource';
import { ProductService } from '../../../shared/services/product.service';
import { ProductCard } from '../../../shared/models/product-card';

import { UUID } from 'angular2-uuid';

@Component({
    selector: 'products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit, OnDestroy
{
    products: ProductResource[] = [];
    filteredProducts: ProductResource[] = [];
    productCards: ProductCard[] = [];
    productSubscription: Subscription;
    category: string | null;

    constructor(private _productService: ProductService, private _route: ActivatedRoute, private _shoppingCartService: ShoppingCartService) { }

    ngOnInit()
    {
        this.productSubscription = this._productService
            .getProducts()
            .subscribe(products =>
            {
                this.products = products;
                this._route.queryParamMap
                    .subscribe(params =>
                    {
                        this.category = params.get('category');
                        this.applyFilter();
                        this.createProductCards(this.filteredProducts);
                    });
            });

    }

    applyFilter()
    {
        this.filteredProducts = (this.category)
            ? this.products.filter(p => p.category.name === this.category)
            : this.products;
    }

    createProductCards(products: ProductResource[])
    {
        this.productCards = [];
        products.forEach(p =>
        {
            let photo = p.productPhotos!.filter(p => p.activated == true)[0];
            let imageUrl = (photo == undefined || photo.thumbnail == undefined)
                ? ''
                : UploadFolderPath.ProductPhotos + photo.thumbnail;

            let stockMessage = p.stock <= 0 ? 'Check back later' : 'In stock';

            let pcard = <ProductCard>{
                id: p.id,
                title: p.name,
                descripton: p.description,
                price: p.price,
                imageUrl: imageUrl,
                link: '/product/view/' + p.id,
                inStock: p.stock > 0,
                stockMessage: stockMessage
            };
            this.productCards.push(pcard);
        });
    }

    addToCart(productId: number)
    {
        let product = this.products.filter(p => p.id === productId)[0];
        let itemId = Math.floor((Math.random() * 10000) + 1);
        let cartItem: CartItemResource = {
            id: itemId,
            productId: productId,
            quantity: 1
        };
        if (product) this._shoppingCartService.addItem(cartItem);

        console.log("cart item: ", cartItem);
    }

    ngOnDestroy()
    {
        if (this.productSubscription)
            this.productSubscription.unsubscribe();
    }
}