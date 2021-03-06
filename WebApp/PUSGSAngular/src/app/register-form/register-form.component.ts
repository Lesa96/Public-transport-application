import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FormArray } from '@angular/forms';
import { RegisterService } from '../register.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent {

  passengerTypes = ["Student" , "Pensioner", "Regular"];
  invalidPass = false;

  registerForm = this.fb.group({
    email: ['', Validators.compose([Validators.required, 
      Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')])
     ],
    password: ["", [Validators.required,Validators.minLength(6)]],
    confirmPassword: [, [Validators.required , Validators.minLength(6)]],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    birthDate: ['', Validators.required],
    address: ['', Validators.required],
    passengerType: this.passengerTypes[2]
  });

  get f() { return this.registerForm.controls; }
  
  constructor(private registerService : RegisterService, private fb: FormBuilder, private router : Router) { }

  onSubmit() {
    
    if(this.registerForm.value.password == this.registerForm.value.confirmPassword)
    {
      this.invalidPass = false;

      localStorage.setItem('email', this.registerForm.value.email);
      this.registerService.register(this.registerForm.value).subscribe(() => {
      if (this.registerForm.value.passengerType != this.passengerTypes[2])
      {
        this.router.navigate(['/register/uploaddocument']);
      }
    });
    }
    else
    {
      this.invalidPass = true;
    }

    
  }
  
}
