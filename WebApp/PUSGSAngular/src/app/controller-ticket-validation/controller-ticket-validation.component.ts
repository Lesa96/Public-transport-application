import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { TicketService } from '../ticket.service';

@Component({
  selector: 'app-controller-ticket-validation',
  templateUrl: './controller-ticket-validation.component.html',
  styleUrls: ['./controller-ticket-validation.component.css']
})
export class ControllerTicketValidationComponent implements OnInit {

  tickets: any[] = [];
  validity = ["Cancelled", "Valid"];

  checkForm = this.fb.group(
    {
      ticketId: ['', Validators.required]
    }
  )

  constructor(private ticketService: TicketService, private fb: FormBuilder) { }

  ngOnInit() {
    this.getTickets();
  }

  onSubmit() {
    this.checkTicketValidity();
  }

  getTickets() {
    this.ticketService.getTickets().subscribe(tickets => {
      this.tickets = tickets;
    });
  }

  checkTicketValidity() {
    this.ticketService.checkTicketValidity(this.checkForm.value).subscribe(ticket => {
      alert("Ticket status: " + this.validity[+ticket.IsValid]);
      window.location.reload();
    });
  }

}
