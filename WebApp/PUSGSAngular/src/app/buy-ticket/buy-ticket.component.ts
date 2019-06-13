import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { BuyTicketService } from '../buy-ticket.service';

@Component({
  selector: 'app-buy-ticket',
  templateUrl: './buy-ticket.component.html',
  styleUrls: ['./buy-ticket.component.css']
})
export class BuyTicketComponent {

  ticketType = ["OneHourTicket", "Daily", "Monthly", "Annual"];

  buyForm = this.fb.group({
    email: ['', Validators.compose([Validators.required, 
      Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')])
     ],
  });

  get f() { return this.buyForm.controls; }

  constructor(private buyTicketService : BuyTicketService, private fb: FormBuilder) { }

  onSubmit()
  {
    this.buyTicketService.buyUnregistered(this.buyForm.value).subscribe();
  }

}
