import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from 'src/app/home/home.component';
import { RegisterComponent } from 'src/app/register/register.component';
import { BuyTicketComponent } from 'src/app/buy-ticket/buy-ticket.component';
import { DrivingPlanComponent } from 'src/app/driving-plan/driving-plan.component';

const routes: Routes = [
  { 
    path: '', 
    redirectTo: '/home', 
    pathMatch: 'full' 
  },
  { 
    path: 'home', 
    component: HomeComponent 
  },
  { 
    path: 'register', 
    component: RegisterComponent 
  },
  { 
    path: 'buyticket', 
    component: BuyTicketComponent 
  },
  { 
    path: 'drivingplan', 
    component: DrivingPlanComponent 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
