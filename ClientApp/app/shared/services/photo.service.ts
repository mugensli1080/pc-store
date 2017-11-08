import { ProductPhoto } from '../models/product-photo';
import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

export enum PhotoType
{
    Product = 1,
    Specification = 2
}

@Injectable()
export class PhotoService
{
    constructor(private _http: Http) { }

    getPhotos(productId: number)
    {
        return this._http
            .get(this._base(productId))
            .map(res => res.json());
    }

    delete(productId: number, id: number, photoType: PhotoType)
    {
        return this._http
            .delete(this._base(productId, photoType) + '/' + id)
            .map(res => res.json());
    }

    activateProductPhoto(productId: number, id: number)
    {
        return this._http
            .put(this._base(productId, PhotoType.Product) + '/' + id, null)
            .map(res => res.json());
    }

    upload(productId: number, photoType: PhotoType, photo: any)
    {
        let formData = new FormData();
        formData.append('file', photo);
        return this._http
            .post(this._base(productId, photoType), formData)
            .map(res => res.json());
    }

    private _base(productId: number, photoType?: PhotoType)
    {
        let type = '';
        switch (photoType)
        {
            case PhotoType.Product:
                type = '/product';
                break;
            case PhotoType.Specification:
                type = '/specification';
                break;
        }
        return '/api/products/' + productId + '/photos' + type;
    }
}