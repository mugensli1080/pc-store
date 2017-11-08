import { CategoriesService } from '../../../../shared/services/categories.service';
import { Category } from '../../../../shared/models/category';
import { Observable } from 'rxjs/Rx';
import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'product-filter',
    templateUrl: './product-filter.component.html',
    styleUrls: ['./product-filter.component.css']
})
export class ProductFilterComponent implements OnInit
{
    @Input('category') category: string;

    categories$: Observable<() => Category>;

    constructor(private _categoriesService: CategoriesService) { }

    ngOnInit()
    {
        this.categories$ = this._categoriesService.getCategories();
    }

}
