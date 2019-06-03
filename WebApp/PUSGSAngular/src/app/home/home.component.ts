import { Component, OnInit, Input } from '@angular/core';
import {HomeService} from '../home.service'
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  
  driveType = ["Gradski" , "Prigradski"];
  days = ["Ponedeljak", "Utorak" ,"Sreda" , "Cetvrtak" , "Petak" , "Subota" , "Nedelja"];
  drivelineNumber = [];
  driveline = { type : this.driveType[0] , day : this.days[0] , drivelineNumber : 0}

  DrivingPlanForm = this.fb.group(
    {
      type : [this.driveline.type],
      day : [this.driveline.day],
      number : this.driveline.drivelineNumber

    }
  )


  private ticketPrice : number;

  constructor(private homeService : HomeService , private fb: FormBuilder) { }

  onSubmit() {
    console.warn(this.DrivingPlanForm.value);
  }

  ngOnInit() {
    this.getDrivelineNumbers();
  }

  getDrivelineNumbers()
  {
    this.homeService.getDrivelineNumbers().subscribe(numbers => this.drivelineNumber = numbers);
  }

  getTicketPrice()
  { 
    this.homeService.getTicketPrice().subscribe(price => { 
      this.ticketPrice = price;
      console.log("=-=============** " + this.ticketPrice);
    });
  }

}
