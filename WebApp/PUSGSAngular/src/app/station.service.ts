import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AddStationBindingModel, UpdateStationBindingModel } from './Models/AddStationBindingModel';


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
    return this.http.get(this.stationNamesUri);
  }

  GetStationsIdsAnsNames() : Observable<any>
  {
    return this.http.get(this.StationIdsAndNamesUri);

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

    return this.http.get(this.GetStationByIdUri,httpOptions);
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

    return this.http.get(this.GetAllStationUri,httpOptions);
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

    return this.http.patch(this.UpdateStationInfoUri , station , httpOptions);
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
