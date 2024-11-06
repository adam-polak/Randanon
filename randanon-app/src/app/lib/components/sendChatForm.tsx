import { sendChat } from "../util/chatEvents";
import { User } from "../util/definitions";

type UserProp = {
    user: User
}

export default function SendChatForm({ user }: UserProp) {
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