import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DrivingPlanService {
  private drivingPlanUri = "http://localhost:8080/api/DrivingPlan";

  constructor(private http: HttpClient) { }

  getDrivingPlans() : Observable<any>
  {
    return this.http.get(this.drivingPlanUri + "/GetAll").pipe(
      catchError(e => throwError(this.handleError(e,"DrivingPlan")))
    );
  }

  getDrivingPlan(input) : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: new HttpParams().set('id' , input.id)     
    }
    return this.http.get(this.drivingPlanUri + "/GetPlan", httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"DrivingPlan")))
    );
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
    return this.http.post(addUri, bindingModel, httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Driveline")))
    );
  }

  deleteDrivingPlan(bindingModel) : Observable<any>
  {
    let addUri = this.drivingPlanUri + "/DeleteDrivingPlan";
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: {
        "Id" : bindingModel.id
      }
    }
    return this.http.delete(addUri, httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"DrivingPlan")))
    );
  }

  saveDrivingPlan(bindingModel) : Observable<any>
  {
    let addUri = this.drivingPlanUri + "/UpdateDrivingPlan";
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }
    }
    return this.http.put(addUri, bindingModel, httpOptions).pipe(
      catchError(e => throwError(this.handleError(e,"Driveline")))
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
