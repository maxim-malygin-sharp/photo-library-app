import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { MockStore, provideMockStore } from '@ngrx/store/testing';
import { StoreModule } from '@ngrx/store';
import { FormsModule } from '@angular/forms';
import { getImages, loading } from './selectors/images.selectors';
import { By } from '@angular/platform-browser';

describe('AppComponent', () => {
  let store: MockStore;
  const initialState = {
    loading: false,
    images: []
  };

  beforeEach((async() => {
    TestBed.configureTestingModule({
      imports: [
        StoreModule.forRoot(provideMockStore),
        RouterTestingModule,
        FormsModule
      ],
      declarations: [
        AppComponent
      ],
      providers: [
        provideMockStore({ initialState })
      ],
    }).compileComponents();
    

    store = TestBed.inject(MockStore);
  }));

it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'angular-image-library-app'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('angular-image-library-app');
  });

it('should render loading', () => {
      const fixture = TestBed.createComponent(AppComponent);
      store = TestBed.inject(MockStore);
      
      store.overrideSelector(loading, true)
      fixture.detectChanges();
      spyOn(store, 'dispatch').and.callFake(() => {});
    
      const compiled = fixture.nativeElement;
          
      expect(compiled.textContent).toContain('loading...');
    });

it('should render list of images', () => {
      const fixture = TestBed.createComponent(AppComponent);
      store = TestBed.inject(MockStore);
      
      store.overrideSelector(getImages, [{
            title: 'Mocked Title',
            id: 101,
            albumId: 3
          }])
      fixture.detectChanges();
      spyOn(store, 'dispatch').and.callFake(() => {});
    
      const compiled = fixture.nativeElement;
          
      expect(
        fixture.debugElement.queryAll(By.css('.list-group-item'))
          .length
      ).toBe(1);
      
      expect(compiled.textContent).toContain('photo-album 3 [101] Mocked Title');
    });
});
