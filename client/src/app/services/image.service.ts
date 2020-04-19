import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from "../../environments/environment";

@Injectable()
export class ImageService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = environment.apiServerUrl;
  }

  getImageUrls(timeline?: string): Observable<string[]> {
    const options = timeline ?
      { params: new HttpParams().set('timeline', timeline) } : {};  
    return this.http.get<string[]>(`${this.baseUrl}/api/images`, options);
  }

  uploadImage(image: File): Observable<any> {
    const formData = new FormData();

    formData.append('image', image);

    return this.http.post<any>(`${this.baseUrl}/api/Images/upload?folder=_records`, formData);
  }
}