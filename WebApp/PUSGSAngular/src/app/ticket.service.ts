import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private ticketUri = "http://localhost:8080/api/Ticket";

  constructor(private http: HttpClient) { }

  getTickets() : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }     
    }
    return this.http.get(this.ticketUri + "/GetAll", httpOptions);
  }

  checkTicketValidity(ticketId) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
          "Content-type": "application/json",
          "Authorization": "Bearer " + localStorage.jwt
      },
  };
    return this.http.patch(this.ticketUri + "/ValidateTicket", ticketId, httpOptions);
  }
}
