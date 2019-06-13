import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

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
    return this.http.get(this.pricelistUri + "/GetAll", httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Pricelists")))
    );
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
    return this.http.get(this.pricelistUri + "/GetPricelist", httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Pricelist")))
    );
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
    return this.http.get(this.pricelistUri + "/GetPricelistItems", httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"PricelistItems")))
    );
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
    return this.http.post(addUri, bindingModel, httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Pricelist")))
    );
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
    return this.http.delete(addUri, httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Pricelist")))
    );
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
    return this.http.put(addUri, bindingModel, httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Pricelist")))
    );
  }


  private handleError(e: HttpErrorResponse , mess : string) {
    if(e.status == 420)
    {
      alert(mess + " doesn't exist");
    }
    else if (e.status == 409)
    {
      alert("This object has been changed by someone (probably another admin), you should reaload and then try again!");
    }
    else 
      alert(e.error.Message);
    
  }
}
