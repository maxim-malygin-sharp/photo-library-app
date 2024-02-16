import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromImageReducer from '../reducers/images.reducer';

export const getImagesState = createFeatureSelector<fromImageReducer.State>('images');

export const loading = createSelector(
    getImagesState,
    (state: fromImageReducer.State) => state.loading
);

export const getImages = createSelector(
    getImagesState,
    (state: fromImageReducer.State) => state.images
);
