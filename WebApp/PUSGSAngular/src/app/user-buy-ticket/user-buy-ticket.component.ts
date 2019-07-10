import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { BuyTicketService } from '../buy-ticket.service';
import { TicketPricesBindingModel } from '../Models/TicketPricesBindingModel';

declare var initPaypalButton: any;

@Component({
  selector: 'app-user-buy-ticket',
  templateUrl: './user-buy-ticket.component.html',
  styleUrls: ['./user-buy-ticket.component.css']
})
export class UserBuyTicketComponent implements OnInit {

  ticketType = ["OneHourTicket", "Daily", "Monthly", "Annual"];
  status : any;
  prices = new TicketPricesBindingModel();
  showPrices = false;
  showPaypal = false;

  buyForm = this.fb.group({
    email: localStorage.email,
    ticketType: [this.ticketType[0], Validators.required],
  });

  get f() { return this.buyForm.controls; }

  constructor(private buyTicketService : BuyTicketService, private fb: FormBuilder) { }

  ngOnInit()
  {
    this.status = localStorage.status;
    console.warn(this.status);
    this.getTicketPrices();
  }

  onSubmit()
  {
    //this.buyTicketService.buyTicket(this.buyForm.value).subscribe();
  }

  getTicketPrices()
  {
    this.buyTicketService.getTicketPrices(this.buyForm.value.email).subscribe(response =>
      {
        this.prices = response;
        this.showPrices = true;

        initPaypalButton("1",true,this.buyForm.value.email,this.prices.OneHour,"OneHourTicket");
        initPaypalButton("2",true,this.buyForm.value.email,this.prices.Daily,"Daily");
        initPaypalButton("3",true,this.buyForm.value.email,this.prices.Monthly,"Monthly");
        initPaypalButton("4",true,this.buyForm.value.email,this.prices.Annual,"Annual");
      });
  }

}
