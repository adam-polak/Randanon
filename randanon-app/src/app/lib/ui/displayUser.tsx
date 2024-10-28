'use client'

import { getUserCookie, setUserCookie } from "../util/cookies";
import { User } from "../util/definitions";

export default function UserCookie() {
    const user : User = {
        ID: 123,
        UserKey: 456
    }

    setUserCookie(user);

    const user2 = getUserCookie();

    return (
        <div>
            <h1>{user.ID} {user.UserKey}</h1>
            <h1>{user2.ID} {user2.UserKey}</h1>
        </div>
    );
}