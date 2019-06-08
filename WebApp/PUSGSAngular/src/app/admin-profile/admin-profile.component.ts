import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../register.service';
import { FormBuilder, Validators } from '@angular/forms';
import { ProfileService } from '../profile.service';

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
    this.profileService.updateAdminProfile(this.profileForm.value).subscribe();
  }

  onChangePassword() {
    this.profileService.changePassword(this.passwordForm.value).subscribe();
  }

  ngOnInit()
  {
    this.profileService.getAdminProfile().subscribe(profile => {
      console.log(profile);

      this.profileForm.controls['email'].patchValue(profile.Email);
      this.profileForm.controls['firstName'].patchValue(profile.FirstName);
      this.profileForm.controls['lastName'].patchValue(profile.LastName);
      this.profileForm.controls['birthDate'].patchValue(profile.BirthDate.split("T")[0]);
      this.profileForm.controls['address'].patchValue(profile.Address);
    });
  }

}
