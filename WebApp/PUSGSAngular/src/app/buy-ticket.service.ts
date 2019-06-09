import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BuyTicketService {

  private buyUnregisteredUri = "http://localhost:8080/api/Ticket/BuyUnregistered";
  private buyTicketUri = "http://localhost:8080/api/Ticket/BuyTicket"

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

  buyTicket(ticketType) : Observable<any> 
  { 
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }
    }
    return this.http.post(this.buyTicketUri, ticketType, httpOptions);
  }
}
