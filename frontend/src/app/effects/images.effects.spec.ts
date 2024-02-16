import { TestBed } from '@angular/core/testing';
import { provideMockActions } from '@ngrx/effects/testing';
import { Observable } from 'rxjs';

import { ImagesEffects } from './images.effects';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ImagesEffects', () => {
  let actions$: Observable<any>;
  let effects: ImagesEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        ImagesEffects,
        provideMockActions(() => actions$)
      ]
    });
    TestBed.runInInjectionContext(() => {
      effects = TestBed.inject(ImagesEffects);
   });
  });

  it('should be created', () => {
    expect(effects).toBeTruthy();
  });
});
