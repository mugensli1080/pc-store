import { map } from 'rxjs/operator/map';
import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class SubCategoriesService
{
    constructor(private _http: Http) { }

    getSubCategories(categoryId: number)
    {
        return this._http
            .get(this._base(categoryId))
            .map(res => res.json());
    }

    getSubCategory(categoryId: number, id: number)
    {
        return this._http
            .get(this._base(categoryId) + '/' + id)
            .map(res => res.json());
    }

    create(categoryId: number, subCategory: any)
    {
        return this._http
            .post(this._base(categoryId), subCategory)
            .map(res => res.json());
    }

    update(categoryId: number, subCategory: any)
    {
        return this._http
            .put(this._base(categoryId) + '/' + subCategory.id, subCategory)
            .map(res => res.json());
    }

    delete(categoryId: number, id: number)
    {
        return this._http
            .delete(this._base(categoryId) + '/' + id)
            .map(res => res.json());
    }

    private _base(categoryId: number)
    {
        return `/api/categories/${categoryId}/subcategories`;
    }
}