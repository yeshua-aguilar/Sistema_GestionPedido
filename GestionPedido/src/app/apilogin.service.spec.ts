import { TestBed } from '@angular/core/testing';

import { ApiloginService } from './apilogin.service';

describe('ApiloginService', () => {
  let service: ApiloginService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiloginService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
