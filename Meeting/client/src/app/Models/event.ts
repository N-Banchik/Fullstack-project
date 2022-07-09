import { Member } from "./member";
import { Photo } from "./photo";
import { Post } from "./post";

export interface Event {
    id: number;
    eventTitle: string;
    eventDescription: string;
    eventDate: Date;
    eventLocation: string;
    mainPhotoUrl: string;
    eventCreated: Date;
    eventRules: string;
    passed: boolean;
    canceled: boolean;
    creator: Member;
    users:Member[]
    posts:Post[]
    photos:Photo[]

}
