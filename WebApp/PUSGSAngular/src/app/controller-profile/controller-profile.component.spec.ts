import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ControllerProfileComponent } from './controller-profile.component';

describe('ControllerProfileComponent', () => {
  let component: ControllerProfileComponent;
  let fixture: ComponentFixture<ControllerProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ControllerProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ControllerProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
