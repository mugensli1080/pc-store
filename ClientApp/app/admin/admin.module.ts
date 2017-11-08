import { AuthGuardService } from './services/auth-guard.service';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { AdminOrdersComponent } from './components/admin-orders/admin-orders.component';
import { AdminProductsComponent } from './components/admin-products/admin-products.component';
import { CategoriesFormComponent } from './components/categories-form/categories-form.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { ProductFormComponent } from './components/product-form/product-form.component';

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild([
            { path: 'admin/products', component: AdminProductsComponent, canActivate: [AuthGuardService] },
            { path: 'admin/product/new', component: ProductFormComponent, canActivate: [AuthGuardService] },
            { path: 'admin/product/:id', component: ProductFormComponent, canActivate: [AuthGuardService] },
            { path: 'admin/orders', component: AdminOrdersComponent, canActivate: [AuthGuardService] },
            { path: 'admin/categories', component: CategoriesComponent, canActivate: [AuthGuardService] },
            { path: 'admin/category/new', component: CategoriesFormComponent, canActivate: [AuthGuardService] },
            { path: 'admin/category/:id', component: CategoriesFormComponent, canActivate: [AuthGuardService] },
        ])
    ],
    declarations: [
        AdminProductsComponent,
        AdminOrdersComponent,
        ProductFormComponent,
        CategoriesComponent,
        CategoriesFormComponent
    ],
    exports: [
        AdminProductsComponent,
        AdminOrdersComponent,
        ProductFormComponent,
        CategoriesComponent,
        CategoriesFormComponent
    ],
    providers: [
        AuthGuardService
    ]
})
export class AdminModule { }