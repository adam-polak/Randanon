import { useEffect, useState } from "react";
import { ChatModel, User } from "../util/definitions"
import SendChatForm from "./sendChatForm";
import ChatMessage from "./chatMessage";
import { getChatNumbers, getChatsAbove } from "../util/chatEvents";


type ChatBoxProp = {
    user: User
}

export default function ChatBox( { user }: ChatBoxProp) {
    const [messages, setMessages] = useState<ChatModel[]>([]);

    useEffect(() => {
        setInterval(() => {
            getChatNumbers().then( (chatNums: number[]) => {
                const filt = messages.filter(x => chatNums.includes(x.ChatNumber));
                if(filt.length != chatNums.length) {
                    if(filt.length < chatNums.length) {
                        let lastNum;
                        if(filt.length == 0) lastNum = 0;
                        else lastNum = filt.at(filt.length - 1)?.ChatNumber ?? 0;
                        getChatsAbove(lastNum).then(nChats => {
                            const curChatNums = filt.map(c => c.ChatNumber);
                            const filterNChats = nChats.filter(x => filt.map(x => !curChatNums.includes(x.ChatNumber)));
                            filterNChats.forEach(c => filt.push(c));
                            setMessages(filt);
                        });
                    } else {
                        setMessages(filt);
                    }
                }
            });
        }, 2000);
    }, []);

    return (
        <div className="flex-row" style={{margin: "auto", padding: "5%"}}>
            <div className="flex-row" style={{border: "black 1px solid", padding: "2%", height:"80vh", width: "90vw", background: "white", color: "black"}}>
                { 
                    messages.map(x => {
                        return (
                            <ChatMessage key={x.ChatNumber} user={user} chat={x} />
                        );
                        }
                    )
                }
            </div>
            <SendChatForm user={user} />
        </div>
    );
}