export interface SaveProductResource
{
    id: number;
    name: string;
    description: string;
    price: number;
    stock: number;
    productPhotoId?: number;
    specificationPhotoId?: number;
    categoryId: number;
    subCategoryId?: number | null;
    videoLink: string;
}