import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteDrivelineComponent } from './delete-driveline.component';

describe('DeleteDrivelineComponent', () => {
  let component: DeleteDrivelineComponent;
  let fixture: ComponentFixture<DeleteDrivelineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeleteDrivelineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteDrivelineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
