import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from "../../environments/environment";
import { Observable } from "rxjs/Observable";
import { Record } from "../models/record.model";

@Injectable()
export class RecordService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = environment.apiServerUrl;
  }

  getRecords(year: number): Observable<Record[]> {
    const options = { params: new HttpParams().set('year', year.toString()) };
    return this.http.get<Record[]>(`${this.baseUrl}/api/Records`, options);
  }

  insertOrReplaceRecord(record: Record): Observable<Record> {
    return this.http.post<Record>(`${this.baseUrl}/api/Records`, record);
  }

  deleteMoment(year: number, key: string): Observable<{}> {
    return this.http.delete(`${this.baseUrl}/api/Records/${year}/${key}`)
  }
}