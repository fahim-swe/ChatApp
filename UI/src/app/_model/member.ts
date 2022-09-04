
export interface member {
    id: string;
    userName: string;
    fullName: string;
    email: string;
    created: Date;
}

export interface paged {
    pageNumber: number;
    pageSize: number;
    totalPages: number;
    totalRecords: number;
    data: member[];
    succeeded: boolean;
    errors?: any;
    message?: any;
}
