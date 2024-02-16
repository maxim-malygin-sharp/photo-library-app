import { Action, createReducer, on } from '@ngrx/store';
import * as ImageActions from '../actions/images.actions';
import { ImageDto } from '../models/image.model';
export const imagesFeatureKey = 'images';

export interface State {
  images: ImageDto[],
  loading : boolean,
  error: any
}

export const initialState: State = {
  images: [],
  loading : false,
  error: null
};

export const reducer = createReducer(
  initialState,
  on(ImageActions.loadImages, (state) => ({...state,loading: true, error:null})),
  on(ImageActions.loadImagesSuccess, (state, { data }) => ({
    ...state,
    images:data,
    loading: false,
    error: null
  })),
  on(ImageActions.loadImagesFailure, (state,{error}) => ({...state,loading: true, error})),
);
