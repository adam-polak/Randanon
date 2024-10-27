export type User = {
    ID: number;
    UserKey: number;
}

export type Chat = {
    ChatNumber: number;
    Message: string;
}

export class RandanonApResponses {
    public static INVALID_USER = "Invalid user";
}

export const RandanonApi : string = "http://localhost:5195"
//export const RandanonApi : string = ""