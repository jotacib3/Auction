/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AdminGmGuardService } from './admin-gm-guard.service';

describe('Service: AdminGmGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdminGmGuardService]
    });
  });

  it('should ...', inject([AdminGmGuardService], (service: AdminGmGuardService) => {
    expect(service).toBeTruthy();
  }));
});
