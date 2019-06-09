import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-controller-navigation',
  templateUrl: './controller-navigation.component.html',
  styleUrls: ['./controller-navigation.component.css']
})
export class ControllerNavigationComponent implements OnInit {

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
