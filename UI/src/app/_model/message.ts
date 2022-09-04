export interface message {
    id: string;
    senderUsername: string;
    recipientUsername: string;
    content: string;
    messageSent: Date;
    messageRead: Date;
}

export interface usermessage {
    pageNumber: number;
    pageSize: number;
    totalPages: number;
    totalRecords: number;
    data: message[];
    succeeded: boolean;
    errors?: any;
    message?: any;
}