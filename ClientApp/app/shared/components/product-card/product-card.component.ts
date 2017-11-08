import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProductCard } from '../../models/product-card';


@Component({
    selector: 'product-card',
    templateUrl: './product-card.component.html',
    styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent
{
    @Input('product-card') productCard: ProductCard;
    @Output('add-to-cart') addItem: EventEmitter<any> = new EventEmitter();

    addToCart()
    {
        this.addItem.emit(this.productCard.id);
    }
}
