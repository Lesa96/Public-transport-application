import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { AddDrivingPlanFormComponent } from './add-driving-plan-form/add-driving-plan-form.component';
import { EditDrivingPlanFormComponent } from './edit-driving-plan-form/edit-driving-plan-form.component';
import { DeleteDrivingPlanFormComponent } from './delete-driving-plan-form/delete-driving-plan-form.component';
import { AddPricelistFormComponent } from './add-pricelist-form/add-pricelist-form.component';
import { EditPricelistFormComponent } from './edit-pricelist-form/edit-pricelist-form.component';
import { DeletePricelistFormComponent } from './delete-pricelist-form/delete-pricelist-form.component';
import { AddDrivelineComponent } from './add-driveline/add-driveline.component';
import { ChangeDrivelineComponent } from './change-driveline/change-driveline.component';
import { DeleteDrivelineComponent } from './delete-driveline/delete-driveline.component';
import { AddStationComponent } from './add-station/add-station.component';
import { ChangeStationComponent } from './change-station/change-station.component';
import { DeleteStationComponent } from './delete-station/delete-station.component';
import { AdminProfileComponent } from './admin-profile/admin-profile.component';
import { UserNavigationComponent } from './user-navigation/user-navigation.component';
import { UserBuyTicketComponent } from './user-buy-ticket/user-buy-ticket.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { ControllerNavigationComponent } from './controller-navigation/controller-navigation.component';
import { ControllerProfileComponent } from './controller-profile/controller-profile.component';
import { ControllerAccountVerificationComponent } from './controller-account-verification/controller-account-verification.component';
import { ControllerTicketValidationComponent } from './controller-ticket-validation/controller-ticket-validation.component';
import { MapComponent } from './map/map.component';

import { AgmCoreModule } from '@agm/core';
import { AgmDirectionModule } from 'agm-direction'; 
import { TokenInterceptor } from './token.interceptor';

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
    AddDrivingPlanFormComponent,
    EditDrivingPlanFormComponent,
    DeleteDrivingPlanFormComponent,
    AddPricelistFormComponent,
    EditPricelistFormComponent,
    DeletePricelistFormComponent,
    AddDrivelineComponent,
    ChangeDrivelineComponent,
    DeleteDrivelineComponent,
    AddStationComponent,
    ChangeStationComponent,
    DeleteStationComponent,
    AdminProfileComponent,
    UserNavigationComponent,
    UserBuyTicketComponent,
    UserProfileComponent,
    ControllerNavigationComponent,
    ControllerProfileComponent,
    ControllerAccountVerificationComponent,
    ControllerTicketValidationComponent,
    MapComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    AgmCoreModule.forRoot({apiKey: 'AIzaSyDnihJyw_34z5S1KZXp90pfTGAqhFszNJk'}),
    AgmDirectionModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
