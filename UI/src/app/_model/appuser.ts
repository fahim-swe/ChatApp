export interface appuser {
    id: string;
    userName: string;
    fullName: string;
    email: string;
    token: string;
    refreshToken: string;
    expiredTime: Date;
    created: Date;
}
