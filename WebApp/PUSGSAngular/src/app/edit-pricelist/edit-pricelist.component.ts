import { Component, OnInit } from '@angular/core';

export type FormType = 'add' | 'edit' | 'delete';

@Component({
  selector: 'app-edit-pricelist',
  templateUrl: './edit-pricelist.component.html',
  styleUrls: ['./edit-pricelist.component.css']
})
export class EditPricelistComponent{

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
