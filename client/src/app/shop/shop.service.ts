import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/products';

  constructor(private http: HttpClient) {}

  getProducts(shopParams: ShopParams) {
    let { brandId, typeId, sort, pageNumber, pageSize, search } = shopParams;
    let params = new HttpParams();

    if (brandId > 0) params = params.append('brandId', brandId);
    if (typeId) params = params.append('typeId', typeId);
    params = params.append('sort', sort);
    params = params.append('pageIndex', pageNumber);
    params = params.append('pageSize', pageSize);
    if (search) params = params.append('search', search);
    return this.http.get<Pagination<Product[]>>(`${this.baseUrl}`, {
      params,
    });
  }

  getProduct(id: number) {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }
  getBrands() {
    return this.http.get<Brand[]>(`${this.baseUrl}/brands`);
  }

  getTypes() {
    return this.http.get<Brand[]>(`${this.baseUrl}/types`);
  }
}
