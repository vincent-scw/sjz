import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable ,  BehaviorSubject } from 'rxjs';

import { environment } from '../../environments/environment';
import { Record } from '../models/record.model';
import { Timeline, ProtectLevel, PeriodGroupLevel } from '../models/timeline.model';

@Injectable()
export class TimelineService {
	private baseUrl: string;
	private defaultTimeline: Timeline = { id: '', protectLevel: ProtectLevel.public, periodGroupLevel: PeriodGroupLevel.byDay, isCompleted: false };
	activeTimeline$ = new BehaviorSubject<Timeline>(this.defaultTimeline);

	constructor(private http: HttpClient) {
		this.baseUrl = environment.apiServerUrl;
	}

	get activeTimeline(): Timeline {
		return this.activeTimeline$.value;
	}

	set activeTimeline(value: Timeline) {
		this.activeTimeline$.next(value);
	}

	insertOrReplaceMoment(moment: Record): Observable<Record> {
		return this.http.post<Record>(`${this.baseUrl}/api/Moments`, moment);
	}

	deleteMoment(topic: string, date: Date): Observable<{}> {
		return this.http.delete(`${this.baseUrl}/api/Moments/${topic}/${date}`)
	}

	getTimelines(): Observable<Timeline[]> {
		return this.http.get<Timeline[]>(`${this.baseUrl}/api/Timelines`);
	}

	getTimeline(topicKey: string): Observable<Timeline> {
		return this.http.get<Timeline>(`${this.baseUrl}/api/Timelines/${topicKey}`);
	}

	insertOrReplaceTimeline(timeline: Timeline): Observable<Timeline> {
		return this.http.post<Timeline>(`${this.baseUrl}/api/Timelines`, timeline);
	}

	deleteTimeline(topic: string): Observable<{}> {
		return this.http.delete(`${this.baseUrl}/api/Timelines/${topic}`);
	}

	verifyAccessCode(timeline: Timeline): Observable<boolean> {
		return this.http.post<boolean>(`${this.baseUrl}/api/Timelines/${timeline.id}/verify`, timeline);
	}
}