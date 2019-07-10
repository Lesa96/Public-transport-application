import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { BuyTicketService } from '../buy-ticket.service';
import * as paypal from 'src/assets/paypal.js'

declare var initPaypalButton: any;

@Component({
  selector: 'app-buy-ticket',
  templateUrl: './buy-ticket.component.html',
  styleUrls: ['./buy-ticket.component.css']
})
export class BuyTicketComponent {

  ticketType = ["OneHourTicket", "Daily", "Monthly", "Annual"];
  price : any;
  showPrices  = false;

  buyForm = this.fb.group({
    email: ['', Validators.compose([Validators.required, 
      Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')])
     ],
  });

  get f() { return this.buyForm.controls; }

  constructor(private buyTicketService : BuyTicketService, private fb: FormBuilder)
   {
      this.buyTicketService.getTicketPrices("").subscribe(resposne=>
        {
          this.price = resposne;
          this.showPrices = true;
          console.warn(resposne);

          initPaypalButton("0",false,"pusgs.testing@gmail.com",this.price,"OneHourTicket");
        });
   }

  onSubmit()
  {
    //zbog paypal-a je ovo trenutno nepotrebno
    // this.buyTicketService.buyUnregistered(this.buyForm.value).subscribe();
  }

}
