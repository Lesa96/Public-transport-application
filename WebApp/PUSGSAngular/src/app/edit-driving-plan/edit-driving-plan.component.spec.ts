import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDrivingPlanComponent } from './edit-driving-plan.component';

describe('EditDrivingPlanComponent', () => {
  let component: EditDrivingPlanComponent;
  let fixture: ComponentFixture<EditDrivingPlanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditDrivingPlanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditDrivingPlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
