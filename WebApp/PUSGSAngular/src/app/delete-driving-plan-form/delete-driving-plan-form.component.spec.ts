import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteDrivingPlanFormComponent } from './delete-driving-plan-form.component';

describe('DeleteDrivingPlanFormComponent', () => {
  let component: DeleteDrivingPlanFormComponent;
  let fixture: ComponentFixture<DeleteDrivingPlanFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeleteDrivingPlanFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteDrivingPlanFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
