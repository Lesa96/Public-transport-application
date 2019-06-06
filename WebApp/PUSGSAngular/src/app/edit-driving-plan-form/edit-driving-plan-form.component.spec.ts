import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDrivingPlanFormComponent } from './edit-driving-plan-form.component';

describe('EditDrivingPlanFormComponent', () => {
  let component: EditDrivingPlanFormComponent;
  let fixture: ComponentFixture<EditDrivingPlanFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditDrivingPlanFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditDrivingPlanFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
