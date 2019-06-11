import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class TokenInterceptor implements HttpInterceptor{
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const url = 'https://localhost:8080';
        let jwt = localStorage.jwt;
        console.log(req.url);

        const httpsReq = req.clone({
            url: req.url.replace("4200", "8080")
          });

          console.log(httpsReq.url);
        // if(jwt){
        //     req = req.clone({
        //         setHeaders: {
        //             "Authorization": "Bearer " + jwt
        //         }
        //     });
        // }

        return next.handle(httpsReq);
    }
}