'use client'

import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { ChatModel, User } from "../util/definitions";
import { deleteChat, getChatCount, getChatNumbers, getChatsAbove, sendChat } from "../util/chatEvents";

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

    const router = useRouter();

    function handleClick() {
        deleteChat(user, chat.ChatNumber);
        router.push("/delete");
    }

    if(display) {
        return (
            <>
            <button onClick={handleClick} style={{ color: "red", marginRight: "1em"}} >X</button>
            </> 
         );
    } else return (<></>);
}

function ChatMessage({ user, chat } : ChatMessageProp) {
    /* 
        split message to deal with overflow, for each 100 or so characters
        create a newline
    */

    const [displayDeleteButton, setDisplayDeleteButton] = useState(false);
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
            <div style={{display: "flex", flex: "flex-row"}} onMouseOver={hoverChat} onMouseOut={mouseLeave} >
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

    const [chats, setChats] = useState<ChatModel[]>( [] );
    const [firstLoad, setFirstLoad] = useState<boolean>( true );


    useEffect( () => {
        const checkSetChat = async () => {
            const chatCount = await getChatCount();
            if(chats.length < chatCount) {
                const last = chats.at(chats.length - 1);
                let largest = 0;
                if(last != undefined) largest = last.ChatNumber;

                const x : ChatModel[] = await getChatsAbove(largest);
                const y : ChatModel[] = [];

                chats.forEach(element => {
                    y.push(element);
                });

                x.forEach(element => {
                    y.push(element);
                });

                setChats(y);
            } else if(chats.length > chatCount) {
                const chatNums : number[] = await getChatNumbers();
                const filt = chats.filter(x => chatNums.includes(x.ChatNumber));
                setChats(filt);
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

    const messages = chats.map((chat) => {
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