import { useCookies } from "react-cookie";
import { User } from "./definitions";

const [cookies, setCookie, removeCookie] = useCookies(['UserID', 'UserKey']);

export function SetUserCookie(user : User) {
    setCookie(cookies.UserID, user.ID);
    setCookie(cookies.UserKey, user.UserKey);
}

export function RemoveUserCookie() {
    removeCookie(cookies.UserID);
    removeCookie(cookies.UserKey);
}

export function GetUserCookie() : User {
    const user : User = {
        ID: cookies.UserID,
        UserKey: cookies.UserKey
    }

    return user;
}