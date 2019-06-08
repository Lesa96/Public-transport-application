import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PricelistService {
  private pricelistUri = "http://localhost:8080/api/Pricelist";
  
  constructor(private http: HttpClient) { }

  getPricelists() : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }
    }
    return this.http.get(this.pricelistUri + "/GetAll", httpOptions);
  }

  getPricelist(input) : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: new HttpParams().set('id' , input.id)     
    }
    return this.http.get(this.pricelistUri + "/GetPricelist", httpOptions);
  }

  getPricelistItems(input) : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: new HttpParams().set('id' , input.id)     
    }
    return this.http.get(this.pricelistUri + "/GetPricelistItems", httpOptions);
  }

  addPricelist(bindingModel) : Observable<any>
  {
    let addUri = this.pricelistUri + "/AddPricelist";
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }
    }
    return this.http.post(addUri, bindingModel, httpOptions);
  }

  deletePricelist(bindingModel) : Observable<any>
  {
    let addUri = this.pricelistUri + "/DeletePricelist";
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: {
        "Id" : bindingModel.id
      }
    }
    return this.http.delete(addUri, httpOptions);
  }

  savePricelist(bindingModel) : Observable<any>
  {
    let addUri = this.pricelistUri + "/UpdatePricelist";
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }
    }
    return this.http.put(addUri, bindingModel, httpOptions);
  }
}
