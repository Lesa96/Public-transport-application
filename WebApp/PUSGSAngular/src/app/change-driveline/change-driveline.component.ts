import { Component, OnInit } from '@angular/core';
import { DrivelineService } from '../driveline.service';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { Observable, observable } from 'rxjs';
import { DrivelineBindingModel, AddDrivelineBindingModel } from '../Models/AddDrivelineBindingModel';

@Component({
  selector: 'app-change-driveline',
  templateUrl: './change-driveline.component.html',
  styleUrls: ['./change-driveline.component.css']
})
export class ChangeDrivelineComponent implements OnInit {

  drivelines = new Observable<any>();
  numberIdForm : FormGroup;
  selectedLine = new AddDrivelineBindingModel();
  sNames = new Array();
  NacrtajDrugu = false;
  selectedNames = new Array();

  response = new DrivelineBindingModel();
  

  selectForm = this.fb.group(
    {
      id : [, Validators.required]
    }
  )

  constructor(private fb: FormBuilder , private drivelineService : DrivelineService) 
  { 
    this.drivelineService.getDrivelineNumbersAndIds().subscribe(drs => this.drivelines = drs);
  }

  onSelectSubmit()
  {
    this.drivelineService.GetDrivelineNumberById(this.selectForm.value).subscribe(number=> 
      {
        this.selectedLine.Number = number; //broj linije
        this.GetDrivelineStationsNames(); 
      });
    
  }

  private GetDrivelineStationsNames()
  {
    this.drivelineService.GetDrivelineStationsNames(this.selectForm.value).subscribe(names=> 
      {
        this.selectedNames = names; //Cekirane stanice trenutne linije
        this.GetAllStationNames();
      }); 
  }

  private GetAllStationNames()
  {
    this.drivelineService.getStationNames().subscribe(names => //sve moguce stanice
      {
        this.sNames = names;
        this.addCheckboxes();

        this.sNames.forEach(element => {
          console.warn(element);
        });
      });

      this.numberIdForm = this.fb.group({
        id : this.selectForm.value.id,
        number : [this.selectedLine.Number , Validators.required],
        sNames : new FormArray([])
      });
  }

  private addCheckboxes()
  {
      this.sNames.map((o, i) => {  //o je trenutni element, i-index
        
        const control = new FormControl((this.selectedNames.indexOf(o) != -1)); //
        (this.numberIdForm.controls.sNames as FormArray).push(control);
      });

      this.NacrtajDrugu = true;
  }
  
  

  onSubmit()
  {
    this.response.DriveLineId = this.numberIdForm.value.id;
    this.response.DriveLineNumber = this.numberIdForm.value.number;
    this.response.StationNames = new Array<any>();

    for(var i=0; i < this.sNames.length; i++)
    {
      if(this.numberIdForm.controls.sNames.value[i] == true)
      {
        this.response.StationNames.push(this.sNames[i]);
      }
    }

    this.drivelineService.UpdateDriveline(this.response).subscribe();
  }

  ngOnInit() {
    
  }

}
