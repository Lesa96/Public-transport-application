import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
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

  selectedNumber : number
  private ticketPrice : number;
  private departures : Observable<string>;

  constructor(private homeService : HomeService , private fb: FormBuilder) { }

  onSubmit() {
    this.getDrivingPlanDepartures();
    this.selectedNumber = this.DrivingPlanForm.value.number;
    console.warn(this.selectedNumber);   
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


}
