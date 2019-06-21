import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../register.service';
import { FormBuilder, Validators } from '@angular/forms';
import { ProfileService } from '../profile.service';
import { map, catchError } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-admin-profile',
  templateUrl: './admin-profile.component.html',
  styleUrls: ['./admin-profile.component.css']
})
export class AdminProfileComponent implements OnInit {

  profileForm = this.fb.group({
    email: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    birthDate: ['', Validators.required],
    address: ['', Validators.required]
  });

  passwordForm = this.fb.group({
    oldPassword: ['', Validators.required],
    newPassword: ['', Validators.required],
    confirmPassword: ['', Validators.required]
  });

  get f() { return this.profileForm.controls; }
  
  constructor(private profileService : ProfileService, private fb: FormBuilder) { }

  onSubmit() {
    this.profileService.updateAdminProfile(this.profileForm.value).subscribe(res=>
      {
        alert("Succssefuly");
        window.location.reload();
      },
      catchError(e => throwError(this.handleError(e,"Profile")))
    );
  }

  onChangePassword() {
    this.profileService.changePassword(this.passwordForm.value).subscribe(res=>
      {
        alert("Succssefuly");
        window.location.reload();
      },
      catchError(e => throwError(this.handleError(e,"Profile")))
      );
  }

  ngOnInit()
  {
    this.profileService.getAdminProfile().subscribe(profile => {
      this.profileForm.controls['email'].patchValue(profile.Email);
      this.profileForm.controls['firstName'].patchValue(profile.FirstName);
      this.profileForm.controls['lastName'].patchValue(profile.LastName);
      this.profileForm.controls['birthDate'].patchValue(profile.BirthDate.split("T")[0]);
      this.profileForm.controls['address'].patchValue(profile.Address);
    });
  }

  private handleError(e: HttpErrorResponse , mess : string) {
    if(e.status == 404)
    {
      alert(mess + " doesn't exist");
    }
    else if (e.status == 409)
    {
      alert("This object has been changed by someone (probably another admin), you should reaload and then try again!");
    }
    else 
      alert(e.error.Message);
    
  }

}
