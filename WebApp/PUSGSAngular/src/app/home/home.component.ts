import { Component, OnInit, Input, Output, EventEmitter, NgZone } from '@angular/core';
import {HomeService} from '../home.service'
import { FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { DatePipe } from '@angular/common';
import { Validators } from '@angular/forms';
import { NotificationService } from '../notification.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  
  driveType = ["City" , "Suburban"];
  days = ["Monday", "Tuesday","Wednesday", "Thursday","Friday","Saturday","Sunday"];
  drivelineNumber = [];
  driveline = { type : this.driveType[0] , day : this.days[0] , drivelineNumber : this.drivelineNumber[0]}

  DrivingPlanForm = this.fb.group(
    {
      type : [this.driveline.type],
      day : [this.driveline.day],
      number : [this.driveline.drivelineNumber , Validators.required]

    }
  )

  selectedNumber : number
  private ticketPrice : number;
  private departures : Observable<string>;
  isConnected: Boolean;
  notifications: string[];
  time: string;

  constructor(private notifService: NotificationService,private ngZone: NgZone, private homeService : HomeService , private fb: FormBuilder) 
  {
    this.isConnected = false;
    this.notifications = [];
  }

  onSubmit() {
    this.getDrivingPlanDepartures();
    this.selectedNumber = this.DrivingPlanForm.value.number;
    console.warn(this.selectedNumber);   
  }

  ngOnInit() {
    this.getDrivelineNumbers();
    //-----------------------------
    this.checkConnection();
    this.subscribeForNotifications();
    this.subscribeForTime();
    this.notifService.registerForClickEvents();
  }

  getDrivingPlanDepartures()
  {
    this.homeService.getDrivingPlanDepartures(this.DrivingPlanForm.value).subscribe(deps => this.departures = deps as Observable<string> );
  }

  getDrivelineNumbers()
  {
    this.homeService.getDrivelineNumbers().subscribe(numbers => this.drivelineNumber = numbers);
  }

  //-----------------
  private checkConnection(){
    this.notifService.startConnection().subscribe(e => {this.isConnected = e; 
        if (e) {
          this.notifService.StartTimer()
        }
    });
  }

  private subscribeForNotifications () {
    this.notifService.notificationReceived.subscribe(e => this.onNotification(e));
  }

  public onNotification(notif: string) {

    this.ngZone.run(() => { 
      this.notifications.push(notif);  
      console.log(this.notifications);
   });  
 }

 subscribeForTime() {
  this.notifService.registerForTimerEvents().subscribe(e => this.onTimeEvent(e));
}

public onTimeEvent(time: string){
  this.ngZone.run(() => { 
     this.time = time; 
  });  
  console.warn(this.time);
}

public startTimer() {
  this.notifService.StartTimer();
}

public stopTimer() {
  this.notifService.StopTimer();
  this.time = "";
}

Ispis()
{
  console.warn(this.time);
}


}
