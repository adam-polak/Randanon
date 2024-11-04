'use client'

import { useEffect, useState } from "react";
import { ChatModel, User } from "../util/definitions";
import { getChatCount, getChatsAbove, sendChatJson } from "../util/chatEvents";

type UserProp = {
    user: User
}

type ChatMessageProp = {
    chat: ChatModel
}

export function ChatMessage({ chat } : ChatMessageProp) {
    return (
        <div>{ chat.Message }</div>
    );
}

export function MessageForm({ user }: UserProp) {
    function sendMessage(formData: FormData) {
        const message = formData.get("message");
        if(message == null) return;
        const json = `{"User":{"ID":${user.ID}, "UserKey":${user.UserKey}},"Message":"${message}"}`;
        sendChatJson(json)
    }

    return (
        <form action={sendMessage}>
            <input name="message" type="text" style={{padding: "1%", border: "black 1px solid", color: "black", width: "80vw", height: "5vh"}} />
            <button type="submit" style={{background: "red", width: "10vw", height: "5vh", border: "black 1px solid"}}>Send</button>
        </form>
    );
}

export default function ChatBox({ user } : UserProp) {

    let [chats, setChats] = useState<ChatModel[]>( [] );
    let [firstLoad, setFirstLoad] = useState<boolean>( true );


    useEffect( () => {
        const checkSetChat = async () => {
            const chatCount = await getChatCount();
            if(chats.length != chatCount) {
                const last = chats.at(chats.length - 1);
                let largest = 0;
                if(last != undefined) largest = last.ChatNumber;

                let x : ChatModel[] = await getChatsAbove(largest);
                let y : ChatModel[] = [];

                chats.forEach(element => {
                    y.push(element);
                });

                x.forEach(element => {
                    y.push(element);
                });

                setChats(y);
            }
        }

        if(firstLoad) {
            checkSetChat();
            setFirstLoad(false);
        }

        const interval = setInterval(() => {
            checkSetChat();
            return () => clearInterval(interval);
        }, 5000);

    });

    let messages = chats.map((chat) => {
        return (<ChatMessage key={chat.ChatNumber} chat={chat} />);
    });

    return (
        <div className="flex-row" style={{margin: "auto"}}>
            <h1>User: {user.ID} {user.UserKey}</h1>
            <div className="flex-row" style={{border: "black 1px solid", padding: "2%", height:"80vh", width: "90vw", background: "white", color: "black"}}>
                { messages }
            </div>
            <MessageForm user={user} />
        </div>
    );
}