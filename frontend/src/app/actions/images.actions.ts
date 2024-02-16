import { createAction, props } from '@ngrx/store';

export const loadImages = createAction(
  '[Images] LoadImages Images',
  props<{ id: number }>()
);

export const loadImagesSuccess = createAction(
  '[Images] LoadImages Images Success',
  props<{ data: any }>()
);

export const loadImagesFailure = createAction(
  '[Images] LoadImages Images Failure',
  props<{ error: any }>()
);
