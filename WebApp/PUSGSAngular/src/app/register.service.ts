import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private registerUri = "http://localhost:8080/api/Account/Register";

  constructor(private http: HttpClient) { }

  register(user) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
          "Content-type": "application/json"
      },
  };
    return this.http.post(this.registerUri, user, httpOptions).pipe(
      map(res =>{
        alert("Succssefuly!");
        window.location.reload();
     }),
    );
  }
}
