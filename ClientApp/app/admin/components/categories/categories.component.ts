import { Subscription } from 'rxjs/Rx';
import { Component, OnDestroy, OnInit } from '@angular/core';

import { Category } from '../../../shared/models/category';
import { CategoriesService } from '../../../shared/services/categories.service';
import { RTOption } from '../../../shared/models/rt-option';

@Component({
    selector: 'app-categories',
    templateUrl: './categories.component.html',
    styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit, OnDestroy
{
    categories: Category[];
    categoriesSubscription: Subscription;
    rtoption: RTOption;
    constructor(
        private _categoriesService: CategoriesService)
    { }

    ngOnInit()
    {
        this.categoriesSubscription = this._categoriesService
            .getCategories()
            .subscribe(categories =>
            {
                this.categories = categories;
                this.rtoption = {
                    hasThumbnail: false,
                    caption: 'Categories List',
                    columns: ['name'],
                    items: categories,
                    itemLinkUrl: '/admin/category',
                    paging: true,
                    search: true
                };
            });

    }
    ngOnDestroy()
    {
        if (this.categoriesSubscription) this.categoriesSubscription.unsubscribe();
    }
}
