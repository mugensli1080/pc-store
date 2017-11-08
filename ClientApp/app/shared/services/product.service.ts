
import { Observable } from 'rxjs/Rx';
import { map } from 'rxjs/operator/map';
import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

import 'rxjs/add/operator/map';
import { SaveProductResource } from '../models/save-product-resource';

@Injectable()
export class ProductService
{

    private _base = '/api/products';
    constructor(private _http: Http) { }

    getProducts()
    {
        return this._http
            .get(this._base)
            .map(res => res.json());
    }

    getProduct(id: number)
    {
        return this._http
            .get(this._base + '/' + id)
            .map(res => res.json());
    }

    create(product: SaveProductResource)
    {
        console.log(product);

        return this._http
            .post(this._base, product)
            .map(res => res.json());
    }

    update(id: number, product: SaveProductResource)
    {
        return this._http
            .put(this._base + '/' + id, product)
            .map(res => res.json());
    }

    delete(id: number)
    {
        return this._http
            .delete(this._base + '/' + id)
            .map(res => res.json());
    }
}