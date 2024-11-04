'use client'

import { useEffect, useState } from "react";
import { ChatModel, User } from "../util/definitions";
import { getChatCount, getChatsAbove, sendChat } from "../util/chatEvents";

type UserProp = {
    user: User
}

type ChatMessageProp = {
    user: User,
    chat: ChatModel
}

type DeleteMessageProp = {
    display: boolean,
    user: User,
    chat: ChatModel
}

function DeleteChatButton({display, user, chat } : DeleteMessageProp) {
    if(display) {
        return (
            <>
            <button style={{ color: "red", marginRight: "1em"}}>X</button>
            </> 
         );
    } else return (<></>);
}

function ChatMessage({ user, chat } : ChatMessageProp) {
    /* 
        split message to deal with overflow, for each 100 or so characters
        create a newline
    */

    let [displayDeleteButton, setDisplayDeleteButton] = useState(false);
    const message = chat.Message;

    function hoverChat() {
        setDisplayDeleteButton(true);
    }

    function mouseLeave() {
        setDisplayDeleteButton(false);
    }

    if(user.ID == chat.UserID) {
        return (
            <>
            <div style={{display: "flex", flex: "flex-row", width: "100%"}} onMouseOver={hoverChat} onMouseOut={mouseLeave} >
                <DeleteChatButton display={displayDeleteButton} user={user} chat={chat} />
                <div>{ message }</div>
            </div>
            </>
        );
    } else {
        return (
            <><div>{ message }</div></>
        );
    }
}

function MessageForm({ user }: UserProp) {
    function sendMessage(formData: FormData) {
        const message = formData.get("message");
        if(message == null) return;
        sendChat(user, message.toString());
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
        return (<ChatMessage key={chat.ChatNumber} user={user} chat={chat} />);
    });

    return (
        <div className="flex-row" style={{margin: "auto", padding: "5%"}}>
            <div className="flex-row" style={{border: "black 1px solid", padding: "2%", height:"80vh", width: "90vw", background: "white", color: "black"}}>
                { messages }
            </div>
            <MessageForm user={user} />
        </div>
    );
}