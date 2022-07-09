import { Photo } from './photo';

export interface Member {
  id: number;
  userName: string;
  photoUrl: string;
  dateOfBirth: Date;
  city: string;
  country: string;
  photo: Photo;
}
