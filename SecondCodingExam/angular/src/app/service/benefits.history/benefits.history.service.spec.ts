import { TestBed } from '@angular/core/testing';

import { BenefitsHistoryService } from './benefits.history.service';

describe('BenefitsHistoryService', () => {
  let service: BenefitsHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BenefitsHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
