import { Component, OnInit } from '@angular/core';


export type FormType = 'AddDriveline' | 'ChangeDriveline' | 'DeleteDriveline';

@Component({
  selector: 'app-edit-lines',
  templateUrl: './edit-lines.component.html',
  styleUrls: ['./edit-lines.component.css']
})
export class EditLinesComponent implements OnInit {

  constructor() { }

  form : FormType = 'AddDriveline';

  get showAdd()
  {
    return this.form === 'AddDriveline';
  }

  get showChange()
  {
    return this.form === 'ChangeDriveline';
  }

  get showDelete()
  {
    return this.form === 'DeleteDriveline';
  }

  toggleForm(type: FormType) {
    this.form = type;
  }

  ngOnInit() {
  }

}
