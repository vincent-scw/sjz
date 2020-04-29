import { Record } from './record.model';

export enum ProtectLevel {
  public = 'Public',
  protect = 'Protected'
}

export enum PeriodGroupLevel {
  byDay = 'ByDay',
  byMonth = 'ByMonth',
  byYear = 'ByYear'
}

export interface Timeline {
  title?: string;
  description?: string;
  timelineId: string;
  protectLevel: ProtectLevel;
  accessKey?: string;
  periodGroupLevel: PeriodGroupLevel;
  isCompleted: boolean;
  ownerId?: string;
  ownerName?: string;
  items?: Record[];
}
