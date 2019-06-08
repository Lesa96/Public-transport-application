import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FormArray } from '@angular/forms';
import { LoginService } from '../login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent  {
  
    loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  
    get f() { return this.loginForm.controls; }

    constructor(private loginService : LoginService, private fb: FormBuilder, private router: Router) { }
  
    onSubmit() {
      this.loginService.login(this.loginForm.value).subscribe(data => { 
        let jwt = data.access_token;

          let jwtData = jwt.split('.')[1]
          let decodedJwtJsonData = window.atob(jwtData)
          let decodedJwtData = JSON.parse(decodedJwtJsonData)

          let role = decodedJwtData.role

          console.log('jwtData: ' + jwtData)
          console.log('decodedJwtJsonData: ' + decodedJwtJsonData)
          console.log('decodedJwtData: ' + decodedJwtData)
          console.log('Role ' + role)

          localStorage.setItem('jwt', jwt)
          localStorage.setItem('role', role);
          if (role == 'Admin')
          {
            this.router.navigate(['/editlines'])
          }
          else
          {
            this.router.navigate(['/home'])
          }
      });
    }

    
    
  }