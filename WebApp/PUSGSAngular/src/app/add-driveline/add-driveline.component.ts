import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
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

  form: FormGroup;
  sNames  = [
    // { id: 100, name: 'order 1' },
    // { id: 200, name: 'order 2' },
    // { id: 300, name: 'order 3' },
    // { id: 400, name: 'order 4' }
  ];

  constructor(private fb: FormBuilder , private drivelineService : DrivelineService) { 
    this.drivelineService.getStationNames().subscribe(names => this.sNames = names);
    console.warn(this.sNames);

    this.form = this.fb.group({
      number : [this.drivelineNumber , Validators.required],
      sNames : this.sNames
    });

    this.addCheckboxes();
  }

  

   drivelineNumber : number;
  // stationNames : any[] = [];

  // DrivelineForm = this.fb.group(
  //   {
  //     number : [this.drivelineNumber , Validators.required],
  //     names : this.fb.array([
  //     ])
  //     // this.stationNames[0] 
  //   }
  // )

  // getStationName = (index : number) => {
  //   return this.stationNames[index]
  // }

  onSubmit() 
  {
    //this.drivelineService.addDriveline(this.DrivelineForm.value).subscribe();
  }

  ngOnInit() {
    

    

    //this.getStationNames(); 
  }
  private addCheckboxes() {
    //this.drivelineService.getStationNames().subscribe(names => this.sNames = names);
    this.sNames.map((o, i) => {

      const control = new FormControl(); // if first item set to true, else false
      (this.form.controls.sNames as FormArray).push(control);
    });
  }

  // get names() {
  //  // return this.DrivelineForm.get('names') as FormArray;
  // }

  // getStationNames()
  // {
  //   this.drivelineService.getStationNames().subscribe(names => {
  //     this.stationNames = names;
  //     this.stationNames.forEach(element => {
  //       this.names.push(this.fb.control(true))
  //     });
  //   });
  // }

}
