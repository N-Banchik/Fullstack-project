import { EventView } from "./event-view";
import { Guide } from "./guide";
import { Photo } from "./photo";

export interface Hobby {
    id: number;
    hobbyName: string;
    description: string;
    rules: string;
    keyFeatures: string;
    photo:Photo;
    events:EventView[];
    guides:Guide[];
}
