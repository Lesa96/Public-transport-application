import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DrivingPlanService {
  private drivingPlanUri = "http://localhost:8080/api/DrivingPlan";

  constructor(private http: HttpClient) { }

  getDrivingPlans() : Observable<any>
  {
    return this.http.get(this.drivingPlanUri + "/GetAll");
  }

  addDrivingPlan(bindingModel) : Observable<any>
  {
    let addUri = this.drivingPlanUri + "/AddDrivingPlan";
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }
    }
    return this.http.post(addUri, bindingModel, httpOptions);
  }

  deleteDrivingPlan(bindingModel) : Observable<any>
  {
    let addUri = this.drivingPlanUri + "/DeleteDrivingPlan";
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }
    }
    return this.http.post(addUri, bindingModel, httpOptions);
  }

}
