import { Component, OnInit, Input } from '@angular/core';
import {HomeService} from '../home.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  
  
  private ticketPrice : number;

  constructor(private homeService : HomeService) { }

  ngOnInit() {
  }

  test()
  {
    console.log("Radi");
  }

  getTicketPrice()
  { 
    this.homeService.getTicketPrice().subscribe(price => { 
      this.ticketPrice = price;
      console.log("=-=============** " + this.ticketPrice);
    });
  }

}
