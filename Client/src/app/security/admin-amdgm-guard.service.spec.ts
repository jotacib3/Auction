/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AdminAmdgmGuardService } from './admin-amdgm-guard.service';

describe('Service: AdminAmdgmGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdminAmdgmGuardService]
    });
  });

  it('should ...', inject([AdminAmdgmGuardService], (service: AdminAmdgmGuardService) => {
    expect(service).toBeTruthy();
  }));
});
