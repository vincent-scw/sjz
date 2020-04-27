import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";
import { Moment } from "../models/moment.model";

@Injectable()
export class MomentService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = environment.apiServerUrl;
  }

  getRecords(year: number): Observable<Moment[]> {
    const options = { params: new HttpParams().set('year', year.toString()) };
    return this.http.get<Moment[]>(`${this.baseUrl}/api/Records`, options);
  }

  insertOrReplaceRecord(record: Moment): Observable<Moment> {
    return this.http.post<Moment>(`${this.baseUrl}/api/Records`, record);
  }

  deleteMoment(year: number, key: string): Observable<{}> {
    return this.http.delete(`${this.baseUrl}/api/Records/${year}/${key}`)
  }
}