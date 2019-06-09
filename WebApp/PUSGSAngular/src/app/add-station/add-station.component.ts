import { Component, OnInit } from '@angular/core';
import { StationService } from '../station.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddStationBindingModel } from '../Models/AddStationBindingModel';

@Component({
  selector: 'app-add-station',
  templateUrl: './add-station.component.html',
  styleUrls: ['./add-station.component.css']
})
export class AddStationComponent implements OnInit {

  form: FormGroup;
  response = new AddStationBindingModel();

  constructor(private fb: FormBuilder , private stationService : StationService) { }

  ngOnInit() {
    this.form = this.fb.group(
      {
        sName : ['' , Validators.required],
        sAddress : ['', Validators.required],
        X : [ , Validators.required],
        Y : [ , Validators.required],
      });
  }

  onSubmit()
  {
    this.response.StationName = this.form.value.sName;
    this.response.StationAddress = this.form.value.sAddress;
    this.response.X = this.form.value.X;
    this.response.Y = this.form.value.Y;

    this.stationService.AddStation(this.response).subscribe();

    this.form.reset();
  }

}
