import { Injectable } from '@angular/core';
import { ImagesService } from '../apis/images.service';
import * as ImageActions from '../actions/images.actions';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, catchError, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable()
export class ImagesEffects {
  constructor(private actions$: Actions, private imagesService:ImagesService) {}

  loadImages$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ImageActions.loadImages),
      map((action: any) => action.id),
      mergeMap((id: number) => {
        return this.imagesService.loadImages(id).pipe(
          map(data => ImageActions.loadImagesSuccess({ data })),
          catchError(error => of(ImageActions.loadImagesFailure({ error })))
        );
      })
    )
  )
}
