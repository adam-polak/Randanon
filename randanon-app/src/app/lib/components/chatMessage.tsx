import { useState } from "react";
import { deleteChat } from "../util/chatEvents";
import { ChatModel, User } from "../util/definitions";

type ChatMessageProp = {
    user: User,
    chat: ChatModel
}

type DeleteChatButtonProp = {
    display: boolean,
    user: User,
    chat: ChatModel
}

function DeleteChatButton({ display, user, chat }: DeleteChatButtonProp) {
    if(display) {

        function handleClick() {
            deleteChat(user, chat.ChatNumber);
        }

        return (
            <>
            <button onClick={handleClick} style={{ color: "red", marginRight: "1em"}} >X</button>
            </>
        );
    } else {
        return (
            <></>
        );
    }
}

export default function ChatMessage({ user, chat }: ChatMessageProp) {

    const message = chat.Message;
    const [displayDeleteButton, setDisplayDeleteButton] = useState(false);

    if(user.ID == chat.UserID) {
    
        function hoverChat() {
            setDisplayDeleteButton(true);
        }
    
        function mouseLeave() {
            setDisplayDeleteButton(false);
        }

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