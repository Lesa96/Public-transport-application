import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { GetTicketPriceBindingModel } from './Models/GetTicketPriceBindingModel';
import { Observable } from 'rxjs/internal/Observable';

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
    return this.http.get(this.drivelineNumbersUri);
  }

  getDrivingPlanDepartures(selecetdItems : any) : Observable<any>
  {
    let httpOptionsDepartures = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', "Authorization": "Bearer " + localStorage.jwt }),
      params: new HttpParams().set('lineNumber' , selecetdItems.number).set('driveType',selecetdItems.type).set('drivePlanDay',selecetdItems.day)
      
    }

    return this.http.get(this.drivingPlanDeparturesUri,httpOptionsDepartures);
  }

  getTicketPrice() : Observable<any> 
  { 
    return this.http.get(this.priceListUri, httpOptions);
  }


}
