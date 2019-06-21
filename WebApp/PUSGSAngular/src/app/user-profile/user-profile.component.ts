import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { ProfileService } from '../profile.service';
import { DomSanitizer } from '@angular/platform-browser';
import { UserService } from '../user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  documents : any[] = [];
  url: string;
  selectedFile: File = null;

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
  
  constructor(private profileService : ProfileService, private fb: FormBuilder, private sanitizer: DomSanitizer, private userService: UserService) { }

  onSubmit() {
    this.profileService.updateUserProfile(this.profileForm.value).subscribe(res=>
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
    this.profileService.getUserProfile().subscribe(profile => {
      this.profileForm.controls['email'].patchValue(profile.Email);
      this.profileForm.controls['firstName'].patchValue(profile.FirstName);
      this.profileForm.controls['lastName'].patchValue(profile.LastName);
      this.profileForm.controls['birthDate'].patchValue(profile.BirthDate.split("T")[0]);
      this.profileForm.controls['address'].patchValue(profile.Address);
    });
    this.getUserDocuments();
  }

  onImgClick(img)
  {
    let path = img.src.split("8080/")[1];
    console.log(path);
    this.userService.deleteDocument(path).subscribe(() => {
      this.getUserDocuments();
    })
  }

  getUserDocuments() {
    this.userService.getUserDocumentsEmail(localStorage.email).subscribe(documents => {
      documents.forEach(document => {
        this.documents.push(this.sanitizer.bypassSecurityTrustUrl("http://localhost:8080/" + document));
      });
    })
  }

  onSelectFile(event) { // called each time file input changes
    if (event.target.files && event.target.files[0]) {
      this.selectedFile = <File>event.target.files[0];

      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => { // called once readAsDataURL is completed
        this.url = reader.result as string;
      }
    }
  }

  onUpload()
  {
    const fd = new FormData();
    fd.append('image', this.selectedFile, this.selectedFile.name);
    this.userService.uploadDocument(fd, localStorage.email).subscribe(() => {

    })
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
