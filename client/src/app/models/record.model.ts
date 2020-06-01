export interface Record {
  recordId?: string;
  date?: Date;
  content?: string;
}

export interface GroupedRecords {
  group: string;
  records: Record[];
}