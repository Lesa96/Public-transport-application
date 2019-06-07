import { Component, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { DrivingPlanService } from 'src/app/driving-plan.service';
import { FormBuilder } from '@angular/forms';
import { HomeService } from 'src/app/home.service';

@Component({
  selector: 'app-edit-driving-plan-form',
  templateUrl: './edit-driving-plan-form.component.html',
  styleUrls: ['./edit-driving-plan-form.component.css']
})
export class EditDrivingPlanFormComponent implements OnInit {

  plans : any[] = [];
  selectedPlan : any;
  driveType = ["City" , "Suburban"];
  days = ["Monday", "Tuesday","Wednesday", "Thursday","Friday","Saturday","Sunday"];
  drivelineNumbers = [];
  driveline = { type : this.driveType[0], day : this.days[0] , drivelineNumber : this.drivelineNumbers[0]};
  departures : any[] = [];
  
    selectForm = this.fb.group(
      {
        id : ['', Validators.required]
      }
    )

    editForm = this.fb.group(
      {
        id : ['', Validators.required],
        type : ['', Validators.required],
        day : ['', Validators.required],
        number : ['' , Validators.required]
      }
    )
  
    constructor(private homeService : HomeService, private drivingPlanService : DrivingPlanService , private fb: FormBuilder) { }
  
    ngOnInit() {
      this.getDrivingPlans();
      this.getDrivelineNumbers()
    }
  
    getDrivelineNumbers()
    {
      this.homeService.getDrivelineNumbers().subscribe(numbers => {this.drivelineNumbers = numbers; console.log(this.drivelineNumbers)});
    }

    onSelectSubmit() {
      this.getDrivingPlan();
    }

    onEditSubmit() {
      this.saveDrivingPlan();
    }
  
    getDrivingPlans()
    {
      this.drivingPlanService.getDrivingPlans().subscribe(plans => this.plans = plans);
    }
  
    getDrivingPlan()
    {
      this.drivingPlanService.getDrivingPlan(this.selectForm.value).subscribe(plan => {
        this.selectedPlan = plan;

        this.editForm.controls['id'].patchValue(plan.Id);
        this.editForm.controls['type'].patchValue(this.driveType[plan.Type - 1]);
        this.editForm.controls['day'].patchValue(this.days[plan.Day - 1]);
        this.editForm.controls['number'].setValue(plan.Line);
        this.departures = plan.Departures.split(";"); //str.split(" ", 3); 
        console.log(this.editForm.value);
      }
      );
    }

    saveDrivingPlan()
    {
      this.drivingPlanService.saveDrivingPlan(this.editForm).subscribe();
    }
}
