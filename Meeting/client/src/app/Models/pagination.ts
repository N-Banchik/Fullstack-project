export interface Pagination {
    currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
}

//2. create a paginated result class type for the data returned from the server
export class PaginatedResult<T> {
  result: T | undefined;
  pagination: Pagination | undefined;
}

