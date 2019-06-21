import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import {AddDrivelineBindingModel, DrivelineBindingModel} from '../app/Models/AddDrivelineBindingModel'
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

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
    return this.http.get(this.stationNamesUri).pipe(
      catchError(e => throwError(this.handleError(e,"stations")))
    );
  }

  addDriveline(bindingModel : AddDrivelineBindingModel)
  {
    let httpOptions = 
    {
      headers: { "Content-type": "application/json",
      "Authorization": "Bearer " + localStorage.jwt
      }
    }
    return this.http.post(this.AddDrivelineUri, bindingModel, httpOptions).pipe(
      map(res =>{
        alert("Succssefuly!");
        window.location.reload();
     }),
      catchError(e => throwError(this.handleError(e,"")))
    );
  }

  getDrivelineNumbers() : Observable<any>
  {
    return this.http.get(this.GetDrivelineNumbersUri).pipe(
      catchError(e => throwError(this.handleError(e,"Driveline Numbers ")))
    );
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
    return this.http.delete(this.DeleteDrivelineUri, httpOptions).pipe(
      map(res =>{
        alert("Succssefuly!");
        window.location.reload();
     }),
      catchError(e => throwError(this.handleError(e,"Driveline")))
    );
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

    return this.http.get(this.GetDrivelineNumbersAndIdsUri,httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Drivelines")))
    );
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

    return this.http.get(this.GetDrivelineNumberByIdUri,httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Driveline")))
    );
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

    return this.http.get(this.GetDrivelineStationsNamesUri,httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Stations")))
    );
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

    return this.http.get(this.GetStationsByNumberUri,httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Station")))
    );
  }

  UpdateDriveline(bindingModel : DrivelineBindingModel)
  {
    let httpOptions = 
    {
      headers: { "Content-type": "application/json",
      "Authorization": "Bearer " + localStorage.jwt }
    }
    return this.http.patch(this.UpdateDrivelineUri, bindingModel, httpOptions).pipe(
      map(res =>{
        alert("Succssefuly!");
        window.location.reload();
     }),
      catchError(e => throwError(this.handleError(e,"Driveline")))
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
