import { TestBed } from '@angular/core/testing';

import { CalculationsHistoryService } from './calculations.history.service';

describe('CalculationsHistoryService', () => {
  let service: CalculationsHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CalculationsHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
