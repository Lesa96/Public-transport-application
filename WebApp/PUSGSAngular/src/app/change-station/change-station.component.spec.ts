import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeStationComponent } from './change-station.component';

describe('ChangeStationComponent', () => {
  let component: ChangeStationComponent;
  let fixture: ComponentFixture<ChangeStationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeStationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeStationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
