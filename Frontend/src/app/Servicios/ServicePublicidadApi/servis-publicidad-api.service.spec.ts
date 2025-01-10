import { TestBed } from '@angular/core/testing';

import { ServisPublicidadApiService } from './servis-publicidad-api.service';

describe('ServisPublicidadApiService', () => {
  let service: ServisPublicidadApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServisPublicidadApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
