import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FormArray } from '@angular/forms';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent {

  registerForm = this.fb.group({
    email: ['', Validators.required, Validators.email],
    password: ['', Validators.required],
    passwordRetype: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    birthDate: ['', Validators.required],
    address: ['', Validators.required],
  });

  get f() { return this.registerForm.controls; }
  
  constructor(private fb: FormBuilder) { }

  onSubmit() {
    console.warn(this.registerForm.value);
  }
  
}
