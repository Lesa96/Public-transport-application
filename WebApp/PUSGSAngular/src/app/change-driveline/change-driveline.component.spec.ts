import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeDrivelineComponent } from './change-driveline.component';

describe('ChangeDrivelineComponent', () => {
  let component: ChangeDrivelineComponent;
  let fixture: ComponentFixture<ChangeDrivelineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeDrivelineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeDrivelineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
