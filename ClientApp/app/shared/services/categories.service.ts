import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

@Injectable()
export class CategoriesService
{
    private _base = "/api/categories";
    constructor(private _http: Http) { }

    getCategories()
    {
        return this._http
            .get(this._base)
            .map(res => res.json());
    }

    getCategory(id: number)
    {
        return this._http
            .get(this._base + '/' + id)
            .map(res => res.json());
    }

    create(category: any)
    {
        return this._http
            .post(this._base, category)
            .map(res => res.json());
    }

    update(id: number, category: any)
    {
        return this._http
            .put(this._base + '/' + id, category)
            .map(res => res.json());
    }

    delete(id: number)
    {
        return this._http
            .delete(this._base + '/' + id)
            .map(res => res.json());
    }
}