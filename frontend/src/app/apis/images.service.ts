import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImagesService {

  public url = environment.imageLibraryApi
  
  private headers:HttpHeaders= new HttpHeaders({
    'Content-Type':'application/json',
    'Accept':"application/json",
    'Access-Control-Allow-Methods':'GET,POST,PUT,DELETE',
    'Authorization':''
  });

  constructor(private http:HttpClient) { }

  public loadImages(id: number){
    return this.http.get(`${this.url}/photos?albumId=${id}`,{headers :this.headers})
  }
}
