import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { AddStationBindingModel, UpdateStationBindingModel } from './Models/AddStationBindingModel';
import { catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class StationService {

  private stationNamesUri = "http://localhost:8080/api/Station/GetAllStationNames";
  private deleteStationUri = "http://localhost:8080/api/Station/DeleteStation";
  private addStationUri = "http://localhost:8080/api/Station/AddStation";
  private StationIdsAndNamesUri = "http://localhost:8080/api/Station/GetIdsAndStationNames";
  private GetStationByIdUri = "http://localhost:8080/api/Station/GetStationsById"
  private UpdateStationInfoUri = "http://localhost:8080/api/Station/UpdateStationInfo"
  private GetAllStationUri = "http://localhost:8080/api/Station/GetAllStations";

  constructor(private http: HttpClient) { }

  GetAllStationsNames() : Observable<any>
  {
    return this.http.get(this.stationNamesUri).pipe(
      catchError(e => throwError(this.handleError(e,"Stations")))
    );
  }

  GetStationsIdsAnsNames() : Observable<any>
  {
    return this.http.get(this.StationIdsAndNamesUri).pipe(
      catchError(e => throwError(this.handleError(e,"Station")))
    );

  }

  GetStationById(stationId : any) : any
  {

    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: new HttpParams().set('id' , stationId.id)
    }

    return this.http.get(this.GetStationByIdUri,httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Station")))
    );
  }
  GetAllStations() : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
    }

    return this.http.get(this.GetAllStationUri,httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Stations")))
    );
  }

  UpdateStationInfo(station : UpdateStationBindingModel)
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }
    }

    return this.http.patch(this.UpdateStationInfoUri , station , httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Station")))
    );
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

    return this.http.post(this.addStationUri , station , httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Station")))
    );
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
    return this.http.delete(this.deleteStationUri, httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Station")))
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
