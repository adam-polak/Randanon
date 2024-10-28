'use client'

import { User } from "./util/definitions";

export default function getUserFromCookies(cookieStr : string) : User {
    const cookieArr = cookieStr.split(' ');
    let id = 0, key = 0;
    for(let i = 0; i < cookieArr.length; i++) {
        if(!cookieArr[i].startsWith("User")) continue;
        let spl = cookieArr[i].split("=");
        spl = spl[1].split(";");
        if(cookieArr[i].startsWith("UserID")) id = parseInt(spl[0]);
        else key = parseInt(spl[0]);
    }
    
    return {
        ID: id,
        UserKey: key
    }
}