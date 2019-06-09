import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { BuyTicketService } from '../buy-ticket.service';

@Component({
  selector: 'app-user-buy-ticket',
  templateUrl: './user-buy-ticket.component.html',
  styleUrls: ['./user-buy-ticket.component.css']
})
export class UserBuyTicketComponent implements OnInit {

  ticketType = ["OneHourTicket", "Daily", "Monthly", "Annual"];
  status : any;

  buyForm = this.fb.group({
    email: localStorage.email,
    ticketType: ['', Validators.required],
  });

  get f() { return this.buyForm.controls; }

  constructor(private buyTicketService : BuyTicketService, private fb: FormBuilder) { }

  ngOnInit()
  {
    this.status = localStorage.status;
  }

  onSubmit()
  {
    this.buyTicketService.buyTicket(this.buyForm.value).subscribe();
  }

}
