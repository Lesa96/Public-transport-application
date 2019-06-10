import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private accountUri = "http://localhost:8080/api/Account";

  constructor(private http: HttpClient) { }

  getNotVerifiedUsers() : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }     
    }
    return this.http.get(this.accountUri + "/GetNotVerifiedUsers", httpOptions);
  }

  getUserDocuments(user) : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: new HttpParams().set('id' , user.id)     
    }
    return this.http.get(this.accountUri + "/GetUserDocuments", httpOptions);
  }

  verifyUser(id) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
          "Content-type": "application/json",
          "Authorization": "Bearer " + localStorage.jwt
      },
  };
    return this.http.patch(this.accountUri + "/VerifyUser", id, httpOptions);
  }

  denyUser(id) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
          "Content-type": "application/json",
          "Authorization": "Bearer " + localStorage.jwt
      },
  };
    return this.http.patch(this.accountUri + "/DenyUser", id, httpOptions);
  }
}
