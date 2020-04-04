export enum ProtectLevel {
  public = 'Public',
  protect = 'Protected'
}

export enum PeriodGroupLevel {
  any = 'Any',
  byDay = 'ByDay',
  byMonth = 'ByMonth',
  byYear = 'ByYear'
}

export interface Timeline {
  username?: string;
  title?: string;
  topicKey: string;
  protectLevel: ProtectLevel;
  accessKey?: string;
  periodGroupLevel: PeriodGroupLevel;
  isCompleted: boolean;
  startTime?: Date;
}
