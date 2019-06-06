import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { FormBuilder } from '@angular/forms';
import {DrivelineService} from '../driveline.service'

import { observable } from 'rxjs/internal/symbol/observable';
import { Validators } from '@angular/forms';
import { FormArray } from '@angular/forms';

@Component({
  selector: 'app-add-driveline',
  templateUrl: './add-driveline.component.html',
  styleUrls: ['./add-driveline.component.css']
})
export class AddDrivelineComponent implements OnInit {

  constructor(private fb: FormBuilder , private drivelineService : DrivelineService) { }

  drivelineNumber : number;
  stationNames : any[] = [];

  DrivelineForm = this.fb.group(
    {
      number : [this.drivelineNumber , Validators.required],
      names : this.fb.array([
      ])
      // this.stationNames[0] 
    }
  )

  getStationName = (index : number) => {
    return this.stationNames[index]
  }

  onSubmit() 
  {
    this.drivelineService.addDriveline(this.DrivelineForm.value).subscribe();
  }

  ngOnInit() {
    this.getStationNames();
  }

  get names() {
    return this.DrivelineForm.get('names') as FormArray;
  }

  getStationNames()
  {
    this.drivelineService.getStationNames().subscribe(names => {
      this.stationNames = names;
      this.stationNames.forEach(element => {
        this.names.push(this.fb.control(true))
      });
    });
  }

}
