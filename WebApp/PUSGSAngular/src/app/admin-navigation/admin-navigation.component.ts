import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-navigation',
  templateUrl: './admin-navigation.component.html',
  styleUrls: ['./admin-navigation.component.css']
})
export class AdminNavigationComponent implements OnInit {

  constructor(private loginService : LoginService, private router: Router) { }
  
    ngOnInit() {
    }
  
    logout() {
      localStorage.removeItem('jwt');
      localStorage.removeItem('role');
      this.router.navigate(['/home']);
    }
}
