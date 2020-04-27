export interface Record {
    id: string;
    date: Date;
    content?: string;
}

export interface GroupedRecords {
    group: string;
    records: Record[];
}