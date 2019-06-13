import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

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
    return this.http.post(this.buyUnregisteredUri, email, httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Ticket")))
    );
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
    return this.http.post(this.buyTicketUri, ticketType, httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Ticket")))
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
