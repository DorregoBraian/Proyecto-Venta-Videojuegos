import { TestBed } from '@angular/core/testing';

import { ServisLoginService } from './servis-login.service';

describe('ServisLoginService', () => {
  let service: ServisLoginService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServisLoginService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
