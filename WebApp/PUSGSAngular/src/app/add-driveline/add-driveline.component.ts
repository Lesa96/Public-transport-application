import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import {DrivelineService} from '../driveline.service'

import { observable } from 'rxjs/internal/symbol/observable';
import { Validators } from '@angular/forms';
import { FormArray } from '@angular/forms';
import { ArrayType } from '@angular/compiler/src/output/output_ast';
import { AddDrivelineBindingModel } from 'src/app/Models/AddDrivelineBindingModel';

@Component({
  selector: 'app-add-driveline',
  templateUrl: './add-driveline.component.html',
  styleUrls: ['./add-driveline.component.css']
})
export class AddDrivelineComponent implements OnInit {

  form: FormGroup;
  sNames = new Array();
  drivelineNumber : number;

  response = new AddDrivelineBindingModel();

  constructor(private fb: FormBuilder , private drivelineService : DrivelineService) { 
    this.drivelineService.getStationNames().subscribe(names => 
      {
        this.sNames = names;
        this.addCheckboxes();

        this.sNames.forEach(element => {
          console.warn(element);
        });
      });

    this.form = this.fb.group({
      number : [this.drivelineNumber , Validators.required],
      sNames : new FormArray([])
    });
    
   
  }

  onSubmit() 
  {
    this.response.Number = this.form.value.number;
    this.response.StationNames = new Array<any>();

    for(var i=0; i < this.sNames.length; i++)
    {
      if(this.form.controls.sNames.value[i] == true)
      {
        this.response.StationNames.push(this.sNames[i]);
      }
    }
    
    this.drivelineService.addDriveline(this.response).subscribe();

    this.form.reset();
    
  }

  ngOnInit() {
   
  }
  private addCheckboxes()
  {
      this.sNames.map((o, i) => {
        const control = new FormControl(); 
        (this.form.controls.sNames as FormArray).push(control);
      });
  } 

  

}
