import { Component, OnInit } from '@angular/core';
import { StationService } from '../station.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { AddStationBindingModel, UpdateStationBindingModel } from '../Models/AddStationBindingModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-station',
  templateUrl: './change-station.component.html',
  styleUrls: ['./change-station.component.css']
})
export class ChangeStationComponent implements OnInit {

  stations = new Observable<any>();
  idNameForm : FormGroup;
  selectedStation = new AddStationBindingModel();
  NacrtajDrugu = false;
  stationForm : FormGroup;
  selectedId : number;

  response = new UpdateStationBindingModel();


  selectForm = this.fb.group(
    {
      id : [, Validators.required]
    }
  )

  constructor(private fb: FormBuilder , private stationService : StationService , private router: Router) 
  {
    this.stationService.GetStationsIdsAnsNames().subscribe(names=> this.stations = names);
  }

  ngOnInit() {
  }

  onSelectSubmit()
  {
    this.stationService.GetStationById(this.selectForm.value).subscribe(station=> 
      {
        this.selectedId = station.Id //selectovan
        this.selectedStation.StationName = station.Name; //
        this.selectedStation.StationAddress = station.Address;
        this.selectedStation.X = station.X;
        this.selectedStation.Y = station.Y;
        this.selectedStation.RowVersion = station.RowVersion;

        this.stationForm = this.fb.group(
          {
            id : [this.selectedId, Validators.required],
            sName : [ this.selectedStation.StationName, Validators.required],
            sAddress : [this.selectedStation.StationAddress, Validators.required],
            X : [this.selectedStation.X = station.X, Validators.required],
            Y : [this.selectedStation.Y = station.Y, Validators.required]
          }
        )

        this.NacrtajDrugu = true;
      });
    
  }

  onSubmit()
  {
    this.response.Id = this.stationForm.value.id;
    this.response.Name = this.stationForm.value.sName;
    this.response.Address = this.stationForm.value.sAddress;
    this.response.X = this.stationForm.value.X;
    this.response.Y = this.stationForm.value.Y;
    this.response.RowVersion = this.selectedStation.RowVersion;

    this.stationService.UpdateStationInfo(this.response).subscribe(()=>{

      
    }); //trebao bi da mu vrati gresku ako hoce da promeni u neki naziv koji vec postoji

    
    
  }

}
