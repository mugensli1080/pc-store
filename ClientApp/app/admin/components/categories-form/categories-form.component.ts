import { OnDestroy, ViewChild } from '@angular/core';
import { Component, ElementRef, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/Rx';

import { Category } from '../../../shared/models/category';
import { SubCategory } from '../../../shared/models/sub-category';
import { CategoriesService } from '../../../shared/services/categories.service';
import { SubCategoriesService } from '../../../shared/services/sub-categories.service';

@Component({
    selector: 'categories-form',
    templateUrl: './categories-form.component.html',
    styleUrls: ['./categories-form.component.css']
})
export class CategoriesFormComponent implements OnInit, OnDestroy
{
    @ViewChild('newSubCategory') newSubCategory: ElementRef;
    category: Category = <Category>{};
    subCategories: SubCategory[] = <SubCategory[]>[{}];
    fileredSubcategories: SubCategory[];
    categoriesSubscription: Subscription;
    subCategoriesSubscription: Subscription;
    selectedSubCategory: SubCategory = <SubCategory>{
        categoryId: 0,
        id: 0,
        name: ''
    };
    isDisabled = false;

    constructor(
        private _categoriesService: CategoriesService,
        private _subCategoriesService: SubCategoriesService,
        private _router: Router,
        private _route: ActivatedRoute)
    {
        _route.params.subscribe(p => this.category.id = +p['id']);
    }

    ngOnInit()
    {
        if (this.category.id)
            this.categoriesSubscription = this._categoriesService
                .getCategory(this.category.id)
                .subscribe(c =>
                {
                    this.category = c;
                    this.subCategoriesSubscription = this._subCategoriesService
                        .getSubCategories(this.category.id)
                        .subscribe(s => this.subCategories = this.fileredSubcategories = s);
                });
    }

    ngOnDestroy(): void
    {
        if (this.categoriesSubscription) this.categoriesSubscription.unsubscribe();
        if (this.subCategoriesSubscription) this.subCategoriesSubscription.unsubscribe();
    }

    onSearch(param: string)
    {
        this.fileredSubcategories = this.subCategories.filter(s => s.name.toLowerCase().includes(param.toLowerCase()));
    }

    saveCategory()
    {
        let task$ = this.category.id
            ? this._categoriesService.update(this.category.id, this.category)
            : this._categoriesService.create(this.category);
        task$.subscribe(category => this._router.navigate(['/admin/category/', category.id]));
    }

    deleteCategory()
    {
        if (!confirm('Are you sure?')) return;
        this._categoriesService
            .delete(this.category.id)
            .subscribe(x => this._router.navigate(['/admin/categories']));
    }

    saveSubCategory()
    {
        let newSubCateggory = <SubCategory>{
            categoryId: this.category.id,
            id: 0,
            name: this.newSubCategory.nativeElement.value
        };

        let task$ = this.selectedSubCategory.id
            ? this._subCategoriesService.update(this.selectedSubCategory.categoryId, this.selectedSubCategory)
            : this._subCategoriesService.create(newSubCateggory.categoryId, newSubCateggory);

        task$.subscribe(subCateggory =>
        {
            if (!this.subCategories.find(x => x.id === subCateggory.id))
                this.subCategories.push(subCateggory);

            this.newSubCategory.nativeElement.value = '';
            this.clearEdit();
        });
    }

    editSubCategory(subCategory: SubCategory)
    {
        this.selectedSubCategory = subCategory;
        this.isDisabled = true;
    }

    deleteSubCateggory()
    {
        if (!confirm('Are you sure?')) return;
        this._subCategoriesService
            .delete(this.selectedSubCategory.categoryId, this.selectedSubCategory.id)
            .subscribe(id =>
            {
                this.subCategories = this.subCategories.filter(s => s.id != id);
                this.clearEdit();
            });
    }

    clearEdit()
    {
        this.selectedSubCategory = { categoryId: 0, id: 0, name: '' };
        this.isDisabled = false;
    }
}
