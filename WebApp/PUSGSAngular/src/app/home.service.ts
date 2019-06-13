import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { GetTicketPriceBindingModel } from './Models/GetTicketPriceBindingModel';
import { Observable } from 'rxjs/internal/Observable';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

const para = new HttpParams();
para.set('TicketType' , "Daily");
para.set("PassengerType" , "Regular")


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  params: new HttpParams().set('ticketType', 'Daily').set('passengerType', 'Regular'),
  
};



@Injectable({
  providedIn: 'root'
})
export class HomeService {
  
  

  private priceListUri = "http://localhost:8080/api/Pricelist/GetTicketPrice";
  private drivelineNumbersUri = "http://localhost:8080/api/Driveline/GetDrivelineNumbers";
  private drivingPlanDeparturesUri = "http://localhost:8080/api/DrivingPlan/GetDrivingPlanDepartures";
  

  constructor(private http: HttpClient) { }

  getDrivelineNumbers() : Observable<any>
  {
    return this.http.get(this.drivelineNumbersUri).pipe(
      catchError(e => throwError(this.handleError(e,"Driveline numbers ")))
    );
  }

  getDrivingPlanDepartures(selecetdItems : any) : Observable<any>
  {
    let httpOptionsDepartures = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', "Authorization": "Bearer " + localStorage.jwt }),
      params: new HttpParams().set('lineNumber' , selecetdItems.number).set('driveType',selecetdItems.type).set('drivePlanDay',selecetdItems.day)
      
    }

    return this.http.get(this.drivingPlanDeparturesUri,httpOptionsDepartures).pipe(
      catchError(e => throwError(this.handleError(e,"Departures")))
    );
  }

  getTicketPrice() : Observable<any> 
  { 
    return this.http.get(this.priceListUri, httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Tickets")))
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
