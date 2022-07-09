export interface Post {
    id: number;
    content: string;
    creatorId: number;
    creatorUserName: string;
    creatorPhotoUrl: string;
    eventName: string;
    eventId: number;
    dateOfCreation: Date;
    editDate: Date;
}
