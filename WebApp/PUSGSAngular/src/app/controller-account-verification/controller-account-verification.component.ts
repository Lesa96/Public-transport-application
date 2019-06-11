import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { FormBuilder, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-controller-account-verification',
  templateUrl: './controller-account-verification.component.html',
  styleUrls: ['./controller-account-verification.component.css']
})
export class ControllerAccountVerificationComponent implements OnInit {
  users : any[] = [];
  selectedUser : any;
  unsafeURLs: any[] = [];
  documents : any[] = [];

  selectForm = this.fb.group(
    {
      id: ['', Validators.required]
    }
  )
  constructor(private userService: UserService, private fb: FormBuilder, private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.getUsers()
  }

  onSubmit() {
    this.getUserDocuments();
  }

  getUsers() {
    this.userService.getNotVerifiedUsers().subscribe(users => {
      this.users = users;
    });
  }

  getUserDocuments() {
    this.userService.getUserDocuments(this.selectForm.value).subscribe(documents => {
      documents.forEach(document => {
        this.documents.push(this.sanitizer.bypassSecurityTrustUrl("http://localhost:8080/" + document));
      });
      this.selectedUser = this.selectForm.value.id;
    })
  }

  deny() {
    this.userService.denyUser(this.selectForm.value).subscribe(() => {
      console.log("denied");
    })
  }

  verify() {
    this.userService.verifyUser(this.selectForm.value).subscribe(() => {
      console.log("verified");
    })
  }

}
