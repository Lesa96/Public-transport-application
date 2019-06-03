import { Component, OnInit } from '@angular/core';

export type FormType = 'login' | 'register';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent{

  form: FormType = 'login';
  
    get showLogin() {
      return this.form === 'login';
    }
  
    get showRegister() {
      return this.form === 'register';
    }
  
    toggleForm(type: FormType) {
      this.form = type;
    }

}
