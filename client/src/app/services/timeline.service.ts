import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { environment } from '../../environments/environment';
import { Moment } from '../models/moment.model';
import { Timeline, ProtectLevel, PeriodGroupLevel } from '../models/timeline.model';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class TimelineService {
	private baseUrl: string;
	private defaultTimeline: Timeline = { topicKey: '', protectLevel: ProtectLevel.public, periodGroupLevel: PeriodGroupLevel.any, isCompleted: false };
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

	getMoments(topic: string): Observable<Moment[]> {
		const options = { params: new HttpParams().set('timeline', topic) };
		return this.http.get<Moment[]>(`${this.baseUrl}/api/Moments`, options);
	}

	insertOrReplaceMoment(moment: Moment): Observable<Moment> {
		return this.http.post<Moment>(`${this.baseUrl}/api/Moments`, moment);
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
		return this.http.post<boolean>(`${this.baseUrl}/api/Timelines/${timeline.topicKey}/verify`, timeline);
	}
}