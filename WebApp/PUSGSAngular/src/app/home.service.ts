import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { GetTicketPriceBindingModel } from './Models/GetTicketPriceBindingModel';

const para = new HttpParams();
para.set('TicketType' , "Daily");
para.set("PassengerType" , "Regular")


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  
  

  private priceListUri = "http://localhost:8080/api/Pricelist/GetTicketPrice";
  

  constructor(private http: HttpClient) { }

  getTicketPrice() 
  {
    let bindingModel = new GetTicketPriceBindingModel();
    bindingModel.TicketType = 1;
    bindingModel.PassengerType = 2;

  return this.http.get(this.priceListUri, httpOptions);
  }
}
