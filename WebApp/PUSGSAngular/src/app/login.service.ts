import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private loginUri = "http://localhost:8080/OAuth/Token";

  constructor(private http: HttpClient) { }

  login(user) : Observable<any> 
  { 
    let data = `username=${user.email}&password=${user.password}&grant_type=password`;
    let httpOptions = {
      headers: {
          "Content-type": "application/x-www-form-urlencoded"
      },
  };
    return this.http.post(this.loginUri, data, httpOptions);
  }
  
}

