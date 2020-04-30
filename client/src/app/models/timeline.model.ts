import { Record } from './record.model';

export enum ProtectLevel {
  public = 1,
  protect = 2
}

export enum PeriodGroupLevel {
  byDay = 1,
  byMonth = 2,
  byYear = 3
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
  lastChanged?: Date;
  items?: Record[];
}
