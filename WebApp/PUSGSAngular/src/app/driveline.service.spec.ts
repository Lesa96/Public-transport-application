import { TestBed } from '@angular/core/testing';

import { DrivelineService } from './driveline.service';

describe('DrivelineService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DrivelineService = TestBed.get(DrivelineService);
    expect(service).toBeTruthy();
  });
});
