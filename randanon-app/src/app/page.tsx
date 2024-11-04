'use client'

import { useState } from "react";
import getUserFromCookies from "./lib/util/cookies";
import ChatBox from "./lib/ui/chat";
import { createUser, validateUser } from "./lib/util/userEvents";

export default function Home() {

  const cookieUser = getUserFromCookies(document.cookie);

  let [user, setUser] = useState(cookieUser);

  async function setValidUser() {
    let ans = user;
    validateUser(user).then( b => {
      if(b) {
        ans = user;
      } else {
        createUser().then(nUser => {
          if(nUser != null) {
            setUser(nUser);
          }
        });
      }
    });
  }

  setValidUser();

  document.cookie = `UserID=${user.ID}; Max-Age=86400`;
  document.cookie = `UserKey=${user.UserKey}; Max-Age=86400`;
  
  return (
    <main >
      <div className="flex">
        <ChatBox user={user} />
      </div>
    </main>
  );
}