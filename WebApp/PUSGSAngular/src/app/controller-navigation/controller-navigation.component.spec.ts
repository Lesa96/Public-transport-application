import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ControllerNavigationComponent } from './controller-navigation.component';

describe('ControllerNavigationComponent', () => {
  let component: ControllerNavigationComponent;
  let fixture: ComponentFixture<ControllerNavigationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ControllerNavigationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ControllerNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
