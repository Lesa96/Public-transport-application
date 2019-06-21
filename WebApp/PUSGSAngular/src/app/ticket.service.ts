import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

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
    return this.http.get(this.ticketUri + "/GetAll", httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Ticket")))
    );
  }

  checkTicketValidity(ticketId) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
          "Content-type": "application/json",
          "Authorization": "Bearer " + localStorage.jwt
      },
  };
    return this.http.patch(this.ticketUri + "/ValidateTicket", ticketId, httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Ticket")))
    );
  }

  private handleError(e: HttpErrorResponse , mess : string) {
    if(e.status == 404)
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
