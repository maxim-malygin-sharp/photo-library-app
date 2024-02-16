import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as ImageActions from "./actions/images.actions";
import * as ImageSelectors from "./selectors/images.selectors";
import { ImageDto } from './models/image.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'angular-image-library-app';
  images$: Observable<ImageDto[]> | undefined;
  loading$: Observable<boolean> | undefined;
  albumId: number = 0;

  constructor( private readonly store: Store){
  }

  ngOnInit(): void {
    this.images$ = this.store.select(ImageSelectors.getImages);
    this.loading$ = this.store.select(ImageSelectors.loading);
  }

  loadImagesByAlbum()
  {
    if (this.albumId < 0 || !this.albumId)
      return;
    this.store.dispatch(ImageActions.loadImages({ id: this.albumId }));
  }
}
