import { TestBed } from '@angular/core/testing';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ImagesService } from './images.service';

describe('Images.Service', () => {
  let service: ImagesService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    
    TestBed.runInInjectionContext(() => {
      service = TestBed.inject(ImagesService);
   });
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
  
  it('should have getData function', () => {
    expect(service.loadImages).toBeTruthy();
   });
});
