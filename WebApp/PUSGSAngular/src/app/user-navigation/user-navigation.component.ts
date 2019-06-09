import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-navigation',
  templateUrl: './user-navigation.component.html',
  styleUrls: ['./user-navigation.component.css']
})
export class UserNavigationComponent implements OnInit {

  constructor(private router: Router) { }
  
  ngOnInit() {
  }

  logout() {
    localStorage.removeItem('jwt');
    localStorage.removeItem('role');
    localStorage.removeItem('email');
    this.router.navigate(['/home']);
  }
}
