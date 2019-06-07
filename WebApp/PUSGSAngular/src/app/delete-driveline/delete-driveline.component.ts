import { Component, OnInit } from '@angular/core';
import { DrivingPlanService } from 'src/app/driving-plan.service';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import {DrivelineService} from '../driveline.service'

@Component({
  selector: 'app-delete-driveline',
  templateUrl: './delete-driveline.component.html',
  styleUrls: ['./delete-driveline.component.css']
})
export class DeleteDrivelineComponent implements OnInit {

  constructor(private fb : FormBuilder , private drivelineService : DrivelineService) { }

  drivelineNumbers : any[] = [];

  deleteForm = this.fb.group(
    {
      number: [, Validators.required]
    }
  )

  ngOnInit() {
    this.getDrivelineNumbers();
  }

  getDrivelineNumbers()
  {
    this.drivelineService.getDrivelineNumbers().subscribe(numbers=> this.drivelineNumbers = numbers);
  }

  onSubmit()
  {
    this.drivelineService.deleteDriveline(this.deleteForm.value).subscribe();
  }

}
