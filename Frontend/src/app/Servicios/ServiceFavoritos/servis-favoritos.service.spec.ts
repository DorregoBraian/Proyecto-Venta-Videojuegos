import { TestBed } from '@angular/core/testing';

import { ServisFavoritosService } from './servis-favoritos.service';

describe('ServisFavoritosService', () => {
  let service: ServisFavoritosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServisFavoritosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
