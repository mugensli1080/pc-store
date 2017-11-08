import { Auth0Service } from './services/auth0/auth0.service';
import { ShoppingCartService } from './services/shopping-cart.service';
import { ProductFilterComponent } from '../client/components/products/product-filter/product-filter.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { NgbCarouselConfig, NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { ProductCardComponent } from './components/product-card/product-card.component';
import { ResponsiveTableComponent } from './components/responsive-table/responsive-table.component';
import { CategoriesService } from './services/categories.service';
import { PhotoService } from './services/photo.service';
import { ProductService } from './services/product.service';
import { ProgressService } from './services/progress.service';
import { SubCategoriesService } from './services/sub-categories.service';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        NgbModule.forRoot().ngModule,
        RouterModule.forChild([]),
    ],
    declarations: [
        ProductCardComponent,
        ResponsiveTableComponent,
        ProductFilterComponent
    ],
    exports: [
        CommonModule,
        FormsModule,
        HttpModule,
        NgbModule.forRoot().ngModule,
        ProductCardComponent,
        ResponsiveTableComponent,
        RouterModule,
        ProductFilterComponent
    ],
    providers: [
        CategoriesService,
        PhotoService,
        ProductService,
        ProgressService,
        SubCategoriesService,
        NgbCarouselConfig,
        ShoppingCartService,
        Auth0Service
    ]
})
export class SharedModule { }