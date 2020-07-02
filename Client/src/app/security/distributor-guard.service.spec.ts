/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DistributorGuardService } from './distributor-guard.service';

describe('Service: DistributorGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DistributorGuardService]
    });
  });

  it('should ...', inject([DistributorGuardService], (service: DistributorGuardService) => {
    expect(service).toBeTruthy();
  }));
});
