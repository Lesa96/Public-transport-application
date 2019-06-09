import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserBuyTicketComponent } from './user-buy-ticket.component';

describe('UserBuyTicketComponent', () => {
  let component: UserBuyTicketComponent;
  let fixture: ComponentFixture<UserBuyTicketComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserBuyTicketComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserBuyTicketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
