import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import {AddDrivelineBindingModel, DrivelineBindingModel} from '../app/Models/AddDrivelineBindingModel'

@Injectable({
  providedIn: 'root'
})
export class DrivelineService {

  constructor(private http: HttpClient) { }

  private stationNamesUri = "http://localhost:8080/api/Station/GetAllStationNames";
  private AddDrivelineUri = "http://localhost:8080/api/Driveline/AddDriveline";
  private GetDrivelineNumbersUri = "http://localhost:8080/api/Driveline/GetDrivelineNumbers";
  private DeleteDrivelineUri = "http://localhost:8080/api/Driveline/DeleteDriveline";
  private GetDrivelineNumbersAndIdsUri = "http://localhost:8080/api/Driveline/GetDrivelineNumbersAndIds";
  private GetDrivelineNumberByIdUri = "http://localhost:8080/api/Driveline/GetDrivelineNumberById";
  private GetDrivelineStationsNamesUri = "http://localhost:8080/api/Driveline/GetDrivelineStationsNames";
  private UpdateDrivelineUri = "http://localhost:8080/api/Driveline/UpdateDriveline";
  private GetStationsByNumberUri = "http://localhost:8080/api/Driveline/GetStationsByDrivelineNumber";

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

  getDrivelineNumbers() : Observable<any>
  {
    return this.http.get(this.GetDrivelineNumbersUri);
  }

  deleteDriveline(drivelineNumber : any)
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: new HttpParams().set('number' , drivelineNumber.number)
    }
    return this.http.delete(this.DeleteDrivelineUri, httpOptions);
  }

  getDrivelineNumbersAndIds() : Observable<any> 
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }, 
    }

    return this.http.get(this.GetDrivelineNumbersAndIdsUri,httpOptions);
  }

  GetDrivelineNumberById(drivelineId : any) : any
  {

    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: new HttpParams().set('id' , drivelineId.id)
    }

    return this.http.get(this.GetDrivelineNumberByIdUri,httpOptions);
  }

  GetDrivelineStationsNames(drivelineId : any) : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: new HttpParams().set('id' , drivelineId.id)
    }

    return this.http.get(this.GetDrivelineStationsNamesUri,httpOptions);
  }

  GetStationsByNumber(drivelineNumber : any) : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: new HttpParams().set('number' , drivelineNumber)
    }

    return this.http.get(this.GetStationsByNumberUri,httpOptions);
  }

  UpdateDriveline(bindingModel : DrivelineBindingModel)
  {
    let httpOptions = 
    {
      headers: { "Content-type": "application/json"}
    }
    return this.http.patch(this.UpdateDrivelineUri, bindingModel, httpOptions);
  }
}
