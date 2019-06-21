import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';

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

  getUserDocumentsEmail(email) : Observable<any>
  {
    let httpOptions = 
    {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
      },
      params: new HttpParams().set('email' , email)     
    }
    return this.http.get(this.accountUri + "/GetUserDocumentsEmail", httpOptions);
  }

  verifyUser(id) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
          "Content-type": "application/json",
          "Authorization": "Bearer " + localStorage.jwt
      },
  };
    return this.http.patch(this.accountUri + "/VerifyUser", id, httpOptions).pipe(
      map(res =>{
        alert("Succssefuly!");
        window.location.reload();
     }),
    );
  }

  denyUser(id) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
          "Content-type": "application/json",
          "Authorization": "Bearer " + localStorage.jwt
      },
  };
    return this.http.patch(this.accountUri + "/DenyUser", id, httpOptions).pipe(
      map(res =>{
        alert("Succssefuly!");
        window.location.reload();
     }),
    );
  }

  uploadDocument(document, mail) : Observable<any> 
  { 
    let httpOptions = {
      params: new HttpParams().append("email", mail)
  };
    return this.http.post(this.accountUri + "/UploadDocument", document, httpOptions).pipe(
      map(res =>{
        alert("Succssefuly!");
        window.location.reload();
     }),
    );
  }

  deleteDocument(document) : Observable<any> 
  { 
    let httpOptions = {
      headers: {
        "Content-type": "application/json",
        "Authorization": "Bearer " + localStorage.jwt
    },
      params: new HttpParams().append("path", document)
  };
    return this.http.delete(this.accountUri + "/DeleteDocument", httpOptions).pipe(
      map(res =>{
        alert("Succssefuly!");
        window.location.reload();
     }),
    );
  }
}
