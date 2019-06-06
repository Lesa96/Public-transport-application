import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import {AddDrivelineBindingModel} from '../app/Models/AddDrivelineBindingModel'

@Injectable({
  providedIn: 'root'
})
export class DrivelineService {

  constructor(private http: HttpClient) { }

  private stationNamesUri = "http://localhost:8080/api/Station/GetAllStationNames";
  private AddDrivelineUri = "http://localhost:8080/api/Driveline/AddDriveline";

  getStationNames() : Observable<any>
  {
    return this.http.get(this.stationNamesUri);
  }

  addDriveline(bindingModel : AddDrivelineBindingModel)
  {
    let httpOptions = 
    {
      headers: { "Content-type": "application/json"}
    }
    return this.http.post(this.AddDrivelineUri, bindingModel, httpOptions);
  }
}
