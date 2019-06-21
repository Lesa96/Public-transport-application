import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private accountUri = "http://localhost:8080/api/Account";

  constructor(private http: HttpClient) { }

  updateAdminProfile(profile) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
          "Content-type": "application/json",
          "Authorization": "Bearer " + localStorage.jwt
      },
  };
    return this.http.put(this.accountUri + "/UpdateAdminProfile", profile, httpOptions).pipe(
      map(res =>{
        alert("Succssefuly!");
        window.location.reload();
     }),
    );
  }

  updateUserProfile(profile) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
          "Content-type": "application/json",
          "Authorization": "Bearer " + localStorage.jwt
      },
  };
    return this.http.put(this.accountUri + "/UpdateUserProfile", profile, httpOptions);
  }

  updateControllerProfile(profile) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
          "Content-type": "application/json",
          "Authorization": "Bearer " + localStorage.jwt
      },
  };
    return this.http.put(this.accountUri + "/UpdateControllerProfile", profile, httpOptions);
  }

  changePassword(passwords) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
          "Content-type": "application/json",
          "Authorization": "Bearer " + localStorage.jwt
      },
  };
    return this.http.post(this.accountUri + "/ChangePassword", passwords, httpOptions);
  }

  getAdminProfile() : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }     
    }
    return this.http.get(this.accountUri + "/GetAdminProfile", httpOptions);
  }

  getUserProfile() : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }     
    }
    return this.http.get(this.accountUri + "/GetUserProfile", httpOptions);
  }

  getControllerProfile() : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }     
    }
    return this.http.get(this.accountUri + "/GetControllerProfile", httpOptions);
  }

  getVerificationStatus() : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      }     
    }
    return this.http.get(this.accountUri + "/GetVerificationStatus", httpOptions);
  }
}
