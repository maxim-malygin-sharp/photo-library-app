import { TestBed } from "@angular/core/testing";
import { StoreModule } from "@ngrx/store";
import { MockStore, provideMockStore } from "@ngrx/store/testing";
import { take } from "rxjs";
import { ImageDto } from "../models/image.model";
import { getImages } from "./images.selectors";


describe('Images Selectors', () => {
  
  let store: MockStore;
  const initialState = {
    loading: false,
    images: []
  };
  beforeEach((async() => {
    TestBed.configureTestingModule({
      imports: [
        StoreModule.forRoot(provideMockStore)
      ],
      providers: [
        provideMockStore({ initialState })
      ],
    }).compileComponents();
    

    store = TestBed.inject(MockStore);
  }));

  it('should select images from state', () => {
    const images: ImageDto[] = [{
      title: 'Mocked Title',
      id: 101,
      albumId: 3
    }];
    let state = { images: images, loading: false, error: undefined };
    
    expect(getImages.projector(state).length).toBe(images.length);
  });
});
