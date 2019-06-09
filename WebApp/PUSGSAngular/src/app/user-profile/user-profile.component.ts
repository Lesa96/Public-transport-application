import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { ProfileService } from '../profile.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

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
    this.profileService.updateUserProfile(this.profileForm.value).subscribe();
  }

  onChangePassword() {
    this.profileService.changePassword(this.passwordForm.value).subscribe();
  }

  ngOnInit()
  {
    this.profileService.getUserProfile().subscribe(profile => {
      this.profileForm.controls['email'].patchValue(profile.Email);
      this.profileForm.controls['firstName'].patchValue(profile.FirstName);
      this.profileForm.controls['lastName'].patchValue(profile.LastName);
      this.profileForm.controls['birthDate'].patchValue(profile.BirthDate.split("T")[0]);
      this.profileForm.controls['address'].patchValue(profile.Address);
    });
  }

}
