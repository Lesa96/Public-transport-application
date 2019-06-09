import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProfileService } from '../profile.service';

@Component({
  selector: 'app-user-navigation',
  templateUrl: './user-navigation.component.html',
  styleUrls: ['./user-navigation.component.css']
})
export class UserNavigationComponent implements OnInit {

  verificationStatus : any;
  status = ["Processing", "Accepted", "Denied"]

  constructor(private profileService : ProfileService, private router: Router) { }
  
  ngOnInit() {
    this.profileService.getVerificationStatus().subscribe(status => 
      { 
        this.verificationStatus = status;
        localStorage.setItem('status', status);
        console.log(status);
      });
  }

  logout() {
    localStorage.removeItem('jwt');
    localStorage.removeItem('role');
    localStorage.removeItem('email');
    this.router.navigate(['/home']);
  }
}
