import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BuyTicketService {

  private buyUnregisteredUri = "http://localhost:8080/api/Ticket/BuyUnregistered";

  constructor(private http: HttpClient) { }

  buyUnregistered(email) : Observable<any> 
  { 
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
      }
    }
    return this.http.post(this.buyUnregisteredUri, email, httpOptions);
  }
}
