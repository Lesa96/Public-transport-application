import { Component, OnInit } from '@angular/core';

export type FormType = 'AddStation' | 'ChangeStation' | 'DeleteStation';

@Component({
  selector: 'app-edit-stations',
  templateUrl: './edit-stations.component.html',
  styleUrls: ['./edit-stations.component.css']
})
export class EditStationsComponent implements OnInit {

  constructor() { }

  form : FormType = 'AddStation';
  
    get showAdd()
    {
      return this.form === 'AddStation';
    }
  
    get showChange()
    {
      return this.form === 'ChangeStation';
    }
  
    get showDelete()
    {
      return this.form === 'DeleteStation';
    }
  
    toggleForm(type: FormType) {
      this.form = type;
    }

  ngOnInit() {
  }

}
