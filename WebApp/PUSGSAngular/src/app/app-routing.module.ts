import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from 'src/app/home/home.component';
import { RegisterComponent } from 'src/app/register/register.component';
import { BuyTicketComponent } from 'src/app/buy-ticket/buy-ticket.component';
import { UploadDocumentComponent } from './upload-document/upload-document.component';
import { EditLinesComponent } from 'src/app/edit-lines/edit-lines.component';
import { EditStationsComponent } from 'src/app/edit-stations/edit-stations.component';
import { EditDrivingPlanComponent } from 'src/app/edit-driving-plan/edit-driving-plan.component';
import { EditPricelistComponent } from 'src/app/edit-pricelist/edit-pricelist.component';

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
    path: 'register/uploaddocument', 
    component: UploadDocumentComponent
  },
  {
    path: 'editlines',
    component: EditLinesComponent
  },
  {
    path: 'editstations',
    component: EditStationsComponent
  },
  {
    path: 'editdrivingplan',
    component: EditDrivingPlanComponent
  },
  {
    path: 'editpricelist',
    component: EditPricelistComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
