import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { BuyTicketService } from '../buy-ticket.service';

@Component({
  selector: 'app-user-buy-ticket',
  templateUrl: './user-buy-ticket.component.html',
  styleUrls: ['./user-buy-ticket.component.css']
})
export class UserBuyTicketComponent {

  ticketType = ["OneHourTicket", "Daily", "Monthly", "Annual"];
  
  buyForm = this.fb.group({
    email: localStorage.email,
    ticketType: ['', Validators.required],
  });

  get f() { return this.buyForm.controls; }

  constructor(private buyTicketService : BuyTicketService, private fb: FormBuilder) { }

  onSubmit()
  {
    this.buyTicketService.buyTicket(this.buyForm.value).subscribe();
  }

}
