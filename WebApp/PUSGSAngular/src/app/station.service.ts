import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AddStationBindingModel } from './Models/AddStationBindingModel';

@Injectable({
  providedIn: 'root'
})
export class StationService {

  private stationNamesUri = "http://localhost:8080/api/Station/GetAllStationNames";
  private deleteStationUri = "http://localhost:8080/api/Station/DeleteStation";
  private addStationUri = "http://localhost:8080/api/Station/AddStation";

  constructor(private http: HttpClient) { }

  GetAllStationsNames() : Observable<any>
  {
    return this.http.get(this.stationNamesUri);
  }

  AddStation(station : AddStationBindingModel)
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }
    }

    return this.http.post(this.addStationUri , station , httpOptions);
  }

  DeleteStation(name : any)
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: {
        "StationName" : name.stationName
      }
    }
    return this.http.delete(this.deleteStationUri, httpOptions);
  }
}
