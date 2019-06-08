import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StationService {

  private stationNamesUri = "http://localhost:8080/api/Station/GetAllStationNames";
  private deleteStationUri = "http://localhost:8080/api/Station/DeleteStation";

  constructor(private http: HttpClient) { }

  GetAllStationsNames() : Observable<any>
  {
    return this.http.get(this.stationNamesUri);
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
