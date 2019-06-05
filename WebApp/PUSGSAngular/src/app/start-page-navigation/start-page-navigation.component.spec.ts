import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StartPageNavigationComponent } from './start-page-navigation.component';

describe('StartPageNavigationComponent', () => {
  let component: StartPageNavigationComponent;
  let fixture: ComponentFixture<StartPageNavigationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StartPageNavigationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StartPageNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
