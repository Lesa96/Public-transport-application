import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { GetTicketPriceBindingModel } from './Models/GetTicketPriceBindingModel';
import { Observable } from 'rxjs/internal/Observable';

const para = new HttpParams();
para.set('TicketType' , "Daily");
para.set("PassengerType" , "Regular")


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  params: new HttpParams().set('ticketType', 'Daily').set('passengerType', 'Regular')
};

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  
  

  private priceListUri = "http://localhost:8080/api/Pricelist/GetTicketPrice";
  

  constructor(private http: HttpClient) { }

  getTicketPrice() : Observable<any> 
  { 
    return this.http.get(this.priceListUri, httpOptions);
  }
}
