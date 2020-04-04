export interface Moment {
    topicKey: string;
    recordDate: Date;
    content?: string;
}

export interface GroupedMoments {
    group: string;
    moments: Moment[];
}