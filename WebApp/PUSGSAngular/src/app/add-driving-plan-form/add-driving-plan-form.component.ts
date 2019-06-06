import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { HomeService } from 'src/app/home.service';
import { ArrayType } from '@angular/compiler/src/output/output_ast';
import { DrivingPlanService } from 'src/app/driving-plan.service';

@Component({
  selector: 'app-add-driving-plan-form',
  templateUrl: './add-driving-plan-form.component.html',
  styleUrls: ['./add-driving-plan-form.component.css']
})
export class AddDrivingPlanFormComponent implements OnInit {

  driveType = ["City" , "Suburban"];
  days = ["Monday", "Tuesday","Wednesday", "Thursday","Friday","Saturday","Sunday"];
  drivelineNumber = [];
  driveline = { type : this.driveType[0], day : this.days[0] , drivelineNumber : this.drivelineNumber[0]};
  departures : any[] = [];


  addForm = this.fb.group(
    {
      type : [this.driveline.type],
      day : [this.driveline.day],
      number : [this.driveline.drivelineNumber , Validators.required]
    }
  )

  addDepartureForm = this.fb.group(
    {
      time : ['', Validators.required]
    }
  )
  constructor(private homeService : HomeService, private drivingPlanService : DrivingPlanService , private fb: FormBuilder) { }

  onSubmit() {
    //console.warn(this.DrivingPlanForm.value);
    this.addDrivingPlan();
    
  }

  ngOnInit() {
    this.getDrivelineNumbers();
  }

  addDrivingPlan()
  {
    //this.homeService.getDrivingPlanDepartures(this.DrivingPlanForm.value).subscribe(deps => this.departures = deps as Observable<string> );
    let bindingModel = this.addForm.value;
    bindingModel['departures'] = this.departures;
    this.drivingPlanService.addDrivingPlan(bindingModel).subscribe();
  }

  getDrivelineNumbers()
  {
    this.homeService.getDrivelineNumbers().subscribe(numbers => this.drivelineNumber = numbers);
  }

  addDeparture()
  {
    let time = this.addDepartureForm.value.time;
    this.departures.push(time);
  }

}
