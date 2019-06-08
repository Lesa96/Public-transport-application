import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { StationService } from '../station.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-delete-station',
  templateUrl: './delete-station.component.html',
  styleUrls: ['./delete-station.component.css']
})
export class DeleteStationComponent implements OnInit {

  sNames = new Observable<any>();
  deleteForm : FormGroup;  

  constructor(private stationService : StationService , private fb: FormBuilder) { }

  ngOnInit() {
    this.stationService.GetAllStationsNames().subscribe(names =>
      {
        this.sNames = names;
        this.deleteForm = this.fb.group(
          {
            stationName : [, Validators.required]
          }
        )
      });
  }

  onSubmit()
  {
    this.stationService.DeleteStation(this.deleteForm.value).subscribe();

    this.deleteForm.reset();
 

  }

  

}
