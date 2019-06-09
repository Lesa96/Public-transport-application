import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ControllerTicketValidationComponent } from './controller-ticket-validation.component';

describe('ControllerTicketValidationComponent', () => {
  let component: ControllerTicketValidationComponent;
  let fixture: ComponentFixture<ControllerTicketValidationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ControllerTicketValidationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ControllerTicketValidationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
