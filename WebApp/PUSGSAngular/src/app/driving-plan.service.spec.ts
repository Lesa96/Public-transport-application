import { TestBed } from '@angular/core/testing';

import { DrivingPlanService } from './driving-plan.service';

describe('DrivingPlanService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DrivingPlanService = TestBed.get(DrivingPlanService);
    expect(service).toBeTruthy();
  });
});
