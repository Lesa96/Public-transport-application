import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FormArray } from '@angular/forms';
import { RegisterService } from '../register.service';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent {

  passengerTypes = ["Student" , "Pensioner", "Regular"];

  registerForm = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    birthDate: ['', Validators.required],
    address: ['', Validators.required],
    passengerType: this.passengerTypes[2]
  });

  get f() { return this.registerForm.controls; }
  
  constructor(private registerService : RegisterService, private fb: FormBuilder) { }

  onSubmit() {
    this.registerService.register(this.registerForm.value).subscribe();
    console.warn(this.registerForm.value);
  }
  
}
