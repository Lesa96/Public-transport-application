import { Component, OnInit } from '@angular/core';
import { DrivingPlanService } from 'src/app/driving-plan.service';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-delete-driving-plan-form',
  templateUrl: './delete-driving-plan-form.component.html',
  styleUrls: ['./delete-driving-plan-form.component.css']
})
export class DeleteDrivingPlanFormComponent implements OnInit {

  plans : any[] = [];

  deleteForm = this.fb.group(
    {
      id : ['', Validators.required]
    }
  )

  constructor(private drivingPlanService : DrivingPlanService , private fb: FormBuilder) { }

  ngOnInit() {
    this.getDrivingPlans();
  }

  onSubmit() {
    this.deleteDrivingPlan();
  }

  getDrivingPlans()
  {
    this.drivingPlanService.getDrivingPlans().subscribe(plans => this.plans = plans);
  }

  deleteDrivingPlan()
  {
    this.drivingPlanService.deleteDrivingPlan(this.deleteForm.value).subscribe();
  }

}
