export class EventParams {
  date: string ;
  userId: number = 0;
  eventId: number = 0;
  hobbyId: number = 0;
  categoryId: number = 0;
  eventName: string = '';
  pageNumber = 1;
  pageSize = 5;
  /**
   *
   */
  constructor() {
    var event = new Date(Date.now());

    let date = JSON.stringify(event);
    this.date = date.slice(1, 11);

  }
}
