import * as PATH from './../../../shared/config/path';

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/Rx';

import { Category } from '../../../shared/models/category';
import { ProductPhoto } from '../../../shared/models/product-photo';
import { ProductResource } from '../../../shared/models/product-resource';
import { SpecificationPhoto } from '../../../shared/models/specification-photo';
import { SubCategory } from '../../../shared/models/sub-category';
import { ProductService } from '../../../shared/services/product.service';

@Component({
    selector: 'product-view',
    templateUrl: './product-view.component.html',
    styleUrls: ['./product-view.component.css']
})
export class ProductViewComponent implements OnInit
{
    productSubscription: Subscription;
    category: Category;
    product: ProductResource = <ProductResource>{
        id: 0,
        name: '',
        description: '',
        price: 0,
        stock: 0,
        productPhotos: <ProductPhoto[]>[{}],
        specificationPhotos: <SpecificationPhoto[]>[],
        category: <Category>{},
        subCategory: <SubCategory>{},
        videoLink: ''
    };
    thumbnail = '';

    stock = { inStock: false, stockMessage: '' };
    constructor(
        private _productService: ProductService,
        private _route: ActivatedRoute,
        private _router: Router
    )
    {
        _route.params.subscribe(p => this.product.id = +p['id']);
    }

    ngOnInit()
    {
        this.productSubscription = this._productService
            .getProduct(this.product.id)
            .subscribe(product =>
            {
                this.product = product;
                let photo = this.product.productPhotos!.filter(p => p.activated === true)[0];
                this.stock = {
                    inStock: this.product.stock > 0,
                    stockMessage: this.product.stock > 0
                        ? 'In stock'
                        : 'Come back later'
                }
                this.thumbnail = PATH.UploadFolderPath.ProductPhotos + photo!.thumbnail;
            });

        // console.log(this.thumbnail);

    }

    get folderPath()
    {
        return PATH.UploadFolderPath;
    }
}
