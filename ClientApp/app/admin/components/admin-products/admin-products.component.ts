import { UploadFolderPath } from '../../../shared/config/path';
import { Subscription } from 'rxjs/Rx';
import { Component, OnDestroy, OnInit } from '@angular/core';

import { ProductResource } from '../../../shared/models/product-resource';
import { SaveProductResource } from '../../../shared/models/save-product-resource';
import { CategoriesService } from '../../../shared/services/categories.service';
import { ProductService } from '../../../shared/services/product.service';
import { SubCategoriesService } from '../../../shared/services/sub-categories.service';
import { RTOption } from '../../../shared/models/rt-option';

@Component({
    selector: 'admin-products',
    templateUrl: './admin-products.component.html',
    styleUrls: ['./admin-products.component.css']
})
export class AdminProductsComponent implements OnInit, OnDestroy
{
    products: ProductResource[];
    productSubscription: Subscription;
    rtoption: RTOption;

    constructor(
        private _productService: ProductService,
        private _categoriesService: CategoriesService,
        private _subCategoriesService: SubCategoriesService) { }

    ngOnInit()
    {
        this.productSubscription = this._productService
            .getProducts()
            .subscribe(products =>
            {
                this.products = products;
                this.rtoption = {
                    hasThumbnail: true,
                    caption: 'Products List',
                    columns: ['name', 'description', 'stock', 'category', 'subCategory'],
                    items: this.createItems(products),
                    itemLinkUrl: '/admin/product',
                    paging: true,
                    search: true
                }
            });

    }

    ngOnDestroy(): void
    {
        if (this.productSubscription) this.productSubscription.unsubscribe();
    }

    createItems(products: ProductResource[])
    {
        let items: any[] = [];
        products.forEach(product =>
        {
            let thumbnail = product.productPhotos!.filter(p => p.activated == true)[0];
            let thumbnailName =
                (thumbnail == undefined || thumbnail.name == undefined)
                    ? '' :
                    UploadFolderPath.ProductPhotos + thumbnail.name;
            let item = {
                thumbnail: thumbnailName,
                id: product.id,
                name: product.name,
                stock: product.stock,
                description: product.description,
                category: product.category.name,
                subCategory: product.subCategory ? product.subCategory.name : ''
            };
            items.push(item);
        });
        return items;
    }
}