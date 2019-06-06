import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './register/register.component';
import { BuyTicketComponent } from './buy-ticket/buy-ticket.component';
import { RegisterFormComponent } from './register-form/register-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { UploadDocumentComponent } from './upload-document/upload-document.component';
import { StartPageNavigationComponent } from './start-page-navigation/start-page-navigation.component';
import { AdminNavigationComponent } from './admin-navigation/admin-navigation.component';
import { EditLinesComponent } from './edit-lines/edit-lines.component';
import { EditStationsComponent } from './edit-stations/edit-stations.component';
import { EditDrivingPlanComponent } from './edit-driving-plan/edit-driving-plan.component';
import { EditPricelistComponent } from './edit-pricelist/edit-pricelist.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegisterComponent,
    BuyTicketComponent,
    RegisterFormComponent,
    LoginFormComponent,
    UploadDocumentComponent,
    StartPageNavigationComponent,
    AdminNavigationComponent,
    EditLinesComponent,
    EditStationsComponent,
    EditDrivingPlanComponent,
    EditPricelistComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
