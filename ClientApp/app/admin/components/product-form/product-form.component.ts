import { UploadFolderPath } from '../../../shared/config/path';
import { Component, ElementRef, OnDestroy, OnInit, ViewChild, NgZone } from '@angular/core';
import { selector } from 'rxjs/operator/publish';

import { ProductPhoto } from '../../../shared/models/product-photo';
import { SaveProductResource } from '../../../shared/models/save-product-resource';
import { SpecificationPhoto } from '../../../shared/models/specification-photo';
import { PhotoType, PhotoService } from '../../../shared/services/photo.service';
import { Subscription, Observable } from 'rxjs';
import { Progress, ProgressService } from '../../../shared/services/progress.service';
import { ProductService } from '../../../shared/services/product.service';
import { Router, ActivatedRoute } from '@angular/router';
import { CategoriesService } from '../../../shared/services/categories.service';
import { SubCategoriesService } from '../../../shared/services/sub-categories.service';
import { ProductResource } from '../../../shared/models/product-resource';
import { asEnumerable } from 'linq-es2015';

@Component({
    selector: 'app-product-form',
    templateUrl: './product-form.component.html',
    styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit, OnDestroy
{
    @ViewChild('productPhotoInput') productPhotoInput: ElementRef;
    @ViewChild('specPhotoInput') specPhotoInput: ElementRef;
    uploadFolderPath = { ProductPhotos: '', SpecificationPhotos: '' };

    product = <SaveProductResource>{
        id: 0,
        name: '',
        description: '',
        price: 0,
        stock: 0,
        categoryId: 0,
        subCategoryId: 0,
        videoLink: ''
    };
    productSubscription: Subscription;
    photoSubscription: Subscription;

    categories$: Observable<() => any>;
    subCategories$: Observable<() => any>;
    photos = {
        product: <ProductPhoto[]>[{}],
        specification: <SpecificationPhoto[]>[{}]
    };
    productPhoto: ProductPhoto | null;
    progress: Progress | null = { total: 0, percentage: 0 };
    progressTab: number;

    constructor(
        private _productService: ProductService,
        private _router: Router,
        private _route: ActivatedRoute,
        private _progressService: ProgressService,
        private _zone: NgZone,
        private _categoriesService: CategoriesService,
        private _subCategoriesService: SubCategoriesService,
        private _photoService: PhotoService
    )
    {
        _route.params.subscribe(p => this.product.id = +p['id']);
        this.uploadFolderPath = UploadFolderPath;
    }

    ngOnInit()
    {
        if (this.product.id)
            this.productSubscription = this._productService
                .getProduct(this.product.id)
                .subscribe(p =>
                {
                    this.setProduct(p);
                    this.categoryChange(this.product.categoryId);
                });

        this.categories$ = this._categoriesService.getCategories();
        this.categoryChange(this.product.categoryId);
        this.setPhotos();
    }

    ngOnDestroy()
    {
        if (this.productSubscription) this.productSubscription.unsubscribe();
        if (this.photoSubscription) this.photoSubscription.unsubscribe();
    }

    setPhotos()
    {
        this.photoSubscription = this._photoService
            .getPhotos(this.product.id)
            .subscribe(photos =>
            {
                this.photos = photos;
                this.productPhoto = <ProductPhoto>this.photos.product.filter((photo) => photo.activated === true)[0];
            });
    }

    categoryChange(id: number)
    {
        this.subCategories$ = this._subCategoriesService
            .getSubCategories(id);
    }

    setProduct(product: ProductResource)
    {
        this.product.name = product.name;
        this.product.description = product.description;
        this.product.price = product.price;
        this.product.stock = product.stock;
        this.product.categoryId = product.category.id;
        this.product.subCategoryId = product.subCategory.id;
        this.product.videoLink = product.videoLink;
    }

    save(product: ProductResource)
    {
        let result$ = (this.product.id)
            ? this._productService.update(this.product.id, this.product)
            : this._productService.create(this.product);
        result$.subscribe(p =>
        {
            this._router.navigate(['/admin/product/', p.id])
        });

    }

    deleteProduct()
    {
        if (!confirm('Are you sure?')) return;
        this._productService
            .delete(this.product.id)
            .subscribe(() =>
            {
                this._router.navigate(['/admin/products']);
            });
        this.product = <SaveProductResource>{};
    }

    deleteProductPhoto(id: number)
    {
        if (!confirm('Are you sure?')) return;
        this._photoService
            .delete(this.product.id, id, PhotoType.Product)
            .subscribe(() => this.setPhotos());
    }

    deleteSpecificationPhoto(id: number)
    {
        if (!confirm('Are you sure?')) return;
        this._photoService
            .delete(this.product.id, id, PhotoType.Specification)
            .subscribe(() => this.setPhotos());
    }

    uploadProductPhoto()
    {
        let nativeElement: HTMLInputElement = this.productPhotoInput.nativeElement;
        this.startProgressBar(1);
        this._photoService
            .upload(this.product.id, PhotoType.Product, nativeElement!.files![0])
            .subscribe(photo =>
            {
                this.photos.product.push(photo);
                nativeElement.value = '';
            });
    }

    activateProductPhoto(productPhoto: ProductPhoto)
    {
        productPhoto.activated = !productPhoto.activated;
        this.productPhoto = productPhoto;
        this._photoService
            .activateProductPhoto(this.product.id, this.productPhoto.id)
            .subscribe(p =>
            {
                this.productPhoto = p;
                this.setPhotos();
            });
    }

    uploadSpecPhoto()
    {
        let nativeElement: HTMLInputElement = this.specPhotoInput.nativeElement;
        this.startProgressBar(2);
        this._photoService
            .upload(this.product.id, PhotoType.Specification, nativeElement!.files![0])
            .subscribe(photo =>
            {
                this.photos.specification.push(photo);
                nativeElement.value = '';
            });
    }

    startProgressBar(tabNumber: number)
    {
        this.progressTab = tabNumber;
        this._progressService
            .startTracking()
            .subscribe(
            progress =>
            {
                this._zone.run(() => { this.progress = progress; })
            },
            error => { console.log("error, tracking progress: " + error) },
            () => { this.progress = null; }
            );
    }

}
