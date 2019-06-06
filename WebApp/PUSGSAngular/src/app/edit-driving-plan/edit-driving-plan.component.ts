import { Component, OnInit } from '@angular/core';

export type FormType = 'add' | 'edit' | 'delete';

@Component({
  selector: 'app-edit-driving-plan',
  templateUrl: './edit-driving-plan.component.html',
  styleUrls: ['./edit-driving-plan.component.css']
})
export class EditDrivingPlanComponent{

  form: FormType = 'add';
  
    get showAdd() {
      return this.form === 'add';
    }
  
    get showEdit() {
      return this.form === 'edit';
    }

    get showDelete() {
      return this.form === 'delete'
    }
  
    toggleForm(type: FormType) {
      this.form = type;
    }

}
