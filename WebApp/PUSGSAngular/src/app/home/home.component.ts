import { Component, OnInit, Input } from '@angular/core';
import {HomeService} from '../home.service'
import { FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { DatePipe } from '@angular/common';
import { Validators } from '@angular/forms';

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


  private ticketPrice : number;
  private departures : Observable<string>;

  constructor(private homeService : HomeService , private fb: FormBuilder) { }

  onSubmit() {
    //console.warn(this.DrivingPlanForm.value);
    this.getDrivingPlanDepartures();
    
  }

  ngOnInit() {
    this.getDrivelineNumbers();
  }

  getDrivingPlanDepartures()
  {
    this.homeService.getDrivingPlanDepartures(this.DrivingPlanForm.value).subscribe(deps => this.departures = deps as Observable<string> );
  }

  getDrivelineNumbers()
  {
    this.homeService.getDrivelineNumbers().subscribe(numbers => this.drivelineNumber = numbers);
  }

  // getTicketPrice()
  // { 
  //   this.homeService.getTicketPrice().subscribe(price => { 
  //     this.ticketPrice = price;
  //     console.log("=-=============** " + this.ticketPrice);
  //   });
  // }

}
