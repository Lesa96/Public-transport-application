import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDrivingPlanFormComponent } from './add-driving-plan-form.component';

describe('AddDrivingPlanFormComponent', () => {
  let component: AddDrivingPlanFormComponent;
  let fixture: ComponentFixture<AddDrivingPlanFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddDrivingPlanFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddDrivingPlanFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
