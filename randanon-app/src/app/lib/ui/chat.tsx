'use client'

import { useEffect, useState } from "react";
import { ChatModel, User } from "../util/definitions";
import { getChatCount, getChats } from "../util/chatEvents";

type UserProp = {
    user: User
};

type ChatMessageProp = {
    chat: ChatModel
}

export function ChatMessage({ chat } : ChatMessageProp) {
    return (
        <div>{ chat.Message }</div>
    );
}

export default function ChatBox({ user } : UserProp) {
    let [chats, setChats] = useState<ChatModel[]>( [] );

    useEffect( () => {
        const checkSetChat = async () => {
            const chatCount = await getChatCount();
            if(chats.length < chatCount) {
                chats = await getChats();
                setChats(chats);
            }
        }

        checkSetChat();

    });

    let messages = chats.map((chat) => {
        return (<ChatMessage key={chat.ChatNumber} chat={chat} />);
    });

    return (
        <div>
            <h1>User: {user.ID} {user.UserKey}</h1>
            { messages }
        </div>
    );
}