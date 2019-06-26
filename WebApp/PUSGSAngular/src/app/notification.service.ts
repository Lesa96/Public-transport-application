import { Injectable, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';

declare var $;

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private proxy: any;  
  private proxyName: string = 'notifications';  
  private connection: any;  
  public connectionExists: Boolean; 
  public ConnectionID : string;

  public notificationReceived: EventEmitter < string >;  

  constructor() {  
      this.notificationReceived = new EventEmitter<string>();
      this.connectionExists = false;  
      // create a hub connection  
      this.connection = $.hubConnection("http://localhost:8080/");
      this.connection.qs = { "token" : "Bearer " + localStorage.jwt };
      // create new proxy with the given name 
      this.proxy = this.connection.createHubProxy(this.proxyName);  
      
  }  
 
  // browser console will display whether the connection was successful    
  public startConnection(): Observable<Boolean> { 
      
    return Observable.create((observer) => {
       
        this.connection.start()
        .done((data: any) => {  
            console.log('Now connected ' + data.transport.name + ', connection ID= ' + data.id)
            this.connectionExists = true;
            this.ConnectionID = data.id;

            observer.next(true);
            observer.complete();
        })
        .fail((error: any) => {  
            console.log('Could not connect ' + error);
            this.connectionExists = false;

            observer.next(false);
            observer.complete(); 
        });  
      });
  }

  public registerForClickEvents(): void {  
      
      this.proxy.on('userClicked', (data: string) => {  
           console.log('received notification: ' + data);  
          this.notificationReceived.emit(data);  
      }); 
   }  

  public registerForTimerEvents() : Observable<string> {
      
    return Observable.create((observer) => {

        this.proxy.on('setRealTime', (data: string) => {  
           // console.log('received LINES: ' + data);  //data je to sto dobijamo sa back-a
            observer.next(data);
        });  
    });      
  }

  public StopTimer() {
      this.proxy.invoke("StopTimeServerUpdates");
  }

  public StartTimer() {
      this.proxy.invoke("TimeServerUpdates");
  }

  public AddToGroupe()
  {
      this.proxy.invoke("AddToGroupe");
  }
}
