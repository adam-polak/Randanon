import { RandanonApi, ChatModel, User } from "./definitions";

const apiUrl = `${RandanonApi}/chat`;

export async function getChatCount() : Promise<number> {
    const requestUrl = `${apiUrl}/count`;

    const response = await fetch(requestUrl);

    const result = await response.text();

    return parseInt(result);
}

export async function sendChatJson(json: string) {
    const requestUrl = `${apiUrl}/send`;
    await fetch(requestUrl, {
        method: "POST",
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