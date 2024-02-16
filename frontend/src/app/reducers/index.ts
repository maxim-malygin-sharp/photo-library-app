import {
  ActionReducerMap,
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../environments/environment';
import * as fromImageReducer from './images.reducer'; 


export interface State {
  images : fromImageReducer.State
}

export const reducers: ActionReducerMap<State> = {
  images: fromImageReducer.reducer
};


export const metaReducers: MetaReducer<State>[] = !environment.production ? [] : [];
