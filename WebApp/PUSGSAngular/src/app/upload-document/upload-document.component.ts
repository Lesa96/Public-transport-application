import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-upload-document',
  templateUrl: './upload-document.component.html',
  styleUrls: ['./upload-document.component.css']
})
export class UploadDocumentComponent implements OnInit {

  constructor(private userService : UserService, private router : Router) { }

  url: string;
  selectedFile: File = null;

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

  ngOnInit() {
  }

  onSkip()
  {
    this.router.navigate(['/register']);
    alert("Successful registration. You can login now. Please upload document later.");
  }

  onConfirm()
  {
    const fd = new FormData();
    fd.append('image', this.selectedFile, this.selectedFile.name);
    this.userService.uploadDocument(fd, localStorage.email).subscribe(() => {
      alert("Successful registration. You can login now.");
      this.router.navigate(['/register']);
    })
  }

}
