import { SubCategory } from './sub-category';
import { Category } from './category';
import { ProductPhoto } from './product-photo';
import { SpecificationPhoto } from './specification-photo';

export interface ProductResource
{
    id: number;
    name: string;
    description: string;
    price: number;
    stock: number;
    productPhotos?: ProductPhoto[];
    specificationPhotos?: SpecificationPhoto[];
    category: Category;
    subCategory: SubCategory;
    videoLink: string;
    created?: Date;
    modified?: Date;
}



