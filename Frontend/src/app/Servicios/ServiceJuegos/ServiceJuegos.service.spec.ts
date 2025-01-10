/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ServiceJuegosService } from './ServiceJuegos.service';

describe('Service: ServiceJuegos', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ServiceJuegosService]
    });
  });

  it('should ...', inject([ServiceJuegosService], (service: ServiceJuegosService) => {
    expect(service).toBeTruthy();
  }));
});
