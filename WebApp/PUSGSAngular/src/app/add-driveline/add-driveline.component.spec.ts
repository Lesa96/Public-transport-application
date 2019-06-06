import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDrivelineComponent } from './add-driveline.component';

describe('AddDrivelineComponent', () => {
  let component: AddDrivelineComponent;
  let fixture: ComponentFixture<AddDrivelineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddDrivelineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddDrivelineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
