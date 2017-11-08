import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { CheckOutComponent } from './components/check-out/check-out.component';
import { MyOrdersComponent } from './components/my-orders/my-orders.component';
import { OrderSuccessComponent } from './components/order-success/order-success.component';
import { ProductViewComponent } from './components/product-view/product-view.component';
import { ProductsComponent } from './components/products/products.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild([
            { path: 'check-out', component: CheckOutComponent },
            { path: 'my/orders', component: MyOrdersComponent },
            { path: 'order-success', component: OrderSuccessComponent },
            { path: 'product/view/:id', component: ProductViewComponent },
            { path: 'products', component: ProductsComponent },
            { path: 'shopping-cart', component: ShoppingCartComponent },
        ])
    ],
    declarations: [
        CheckOutComponent,
        MyOrdersComponent,
        OrderSuccessComponent,
        ProductViewComponent,
        ProductsComponent,
        ShoppingCartComponent,
    ],
    exports: [
        CheckOutComponent,
        MyOrdersComponent,
        OrderSuccessComponent,
        ProductViewComponent,
        ProductsComponent,
        ShoppingCartComponent,
    ]
})
export class ClientModule { }