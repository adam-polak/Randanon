import { RandanonApi, ChatModel, User } from "./definitions";

const apiUrl = `${RandanonApi}/chat`;

function getUserJsonObject(user: User) {
    return `"User":{"ID":${user.ID},"UserKey":${user.UserKey}}`;
}

export async function getChatCount() : Promise<number> {
    const requestUrl = `${apiUrl}/count`;

    const response = await fetch(requestUrl);

    const result = await response.text();

    return parseInt(result);
}

export async function sendChat(user: User, message: string) {
    const json = `{${getUserJsonObject(user)},"Message":"${message}"}`;
    const requestUrl = `${apiUrl}/send`;
    await fetch(requestUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: json
    });
}

export async function getChatNumbers() {
    const requestUrl = `${RandanonApi}/chatnumbers/`;
    const response = await fetch(requestUrl);
    const body = await response.text();
    return JSON.parse(body);
}

export async function deleteChat(user: User, chatNumber: number) {
    const json = `{${getUserJsonObject(user)},"ChatNumber":${chatNumber}}`;
    const requestUrl = `${apiUrl}/delete`;

    await fetch(requestUrl, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json"
        },
        body: json
    });
}

export async function getChatsAbove(x : number) : Promise<ChatModel[]> {
    const requestUrl = `${RandanonApi}/chats/${x}`;

    const response = await fetch(requestUrl);
    const result = await response.text();

    try {
        const chats : ChatModel[] = JSON.parse(result);
        return chats;
    } catch {
        return [];
    }
}