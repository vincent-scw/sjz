import { TestBed, getTestBed } from "@angular/core/testing";
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClient } from "@angular/common/http";

import { TimelineService } from "./timeline.service";
import { Observable } from "rxjs/Observable";
import { Timeline, ProtectLevel, PeriodGroupLevel } from "../models/timeline.model";
import { environment } from "../../environments/environment";

describe('TimelineService', () => {
  let injector: TestBed;
  let service: TimelineService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('HttpClient', ['get']);

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TimelineService]
    });

    injector = getTestBed();
    service = injector.get(TimelineService);
    httpMock = injector.get(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  describe('#getTimelines', () => {
    it('should return timelines from the mock httpclient', () => {
      const stubValues: Timeline[] = [
        { id: 'topic1', protectLevel: ProtectLevel.public, periodGroupLevel: PeriodGroupLevel.byYear, isCompleted: true },
        { id: 'topic2', protectLevel: ProtectLevel.public, periodGroupLevel: PeriodGroupLevel.byMonth, isCompleted: false }
      ];

      service.getTimelines().subscribe(timelines => {
        expect(timelines.length).toBe(2);
        expect(timelines).toEqual(stubValues)
      });
      
      const req = httpMock.expectOne(`${environment.apiServerUrl}/api/Timelines`);
      expect(req.request.method).toBe("GET");
      req.flush(stubValues);
    });
  });
});