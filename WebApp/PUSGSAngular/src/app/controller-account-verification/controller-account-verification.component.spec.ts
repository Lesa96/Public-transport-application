import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ControllerAccountVerificationComponent } from './controller-account-verification.component';

describe('ControllerAccountVerificationComponent', () => {
  let component: ControllerAccountVerificationComponent;
  let fixture: ComponentFixture<ControllerAccountVerificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ControllerAccountVerificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ControllerAccountVerificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
