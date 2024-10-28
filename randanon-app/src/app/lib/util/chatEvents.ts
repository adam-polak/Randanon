import { json } from "stream/consumers";
import { RandanonApi, ChatModel, User } from "./definitions";

const apiUrl = `${RandanonApi}/chat`;

export async function getChatCount() : Promise<number> {
    const requestUrl = `${apiUrl}/count`;

    const response = await fetch(requestUrl);

    const result = await response.text();

    return parseInt(result);
}

export async function getChats() : Promise<ChatModel[]> {
    const requestUrl = `${apiUrl}/all`;

    const response = await fetch(requestUrl);
    const result = await response.text();

    try {
        const chats : ChatModel[] = JSON.parse(result);
        return chats;
    } catch {
        return [];
    }
}