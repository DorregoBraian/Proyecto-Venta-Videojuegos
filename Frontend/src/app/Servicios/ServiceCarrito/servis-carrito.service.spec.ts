import { TestBed } from '@angular/core/testing';

import { ServisCarritoService } from './servis-carrito.service';

describe('ServisCarritoService', () => {
  let service: ServisCarritoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServisCarritoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
