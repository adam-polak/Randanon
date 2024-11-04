'use client'

import { useEffect, useState } from "react";
import getUserFromCookies from "./lib/util/cookies";
import ChatBox from "./lib/ui/chat";
import { createUser, validateUser } from "./lib/util/userEvents";

export default function Home() {

  let cookieUser = { ID: 0, UserKey: 0 };
  useEffect(() => {
    cookieUser = getUserFromCookies(document.cookie);
  });

  const [user, setUser] = useState(cookieUser);

  async function setValidUser() {
    validateUser(user).then( valid => {
      if(!valid) {
        createUser().then(nUser => {
          if(nUser != null) {
            setUser(nUser);
          }
        });
      }
    });
  }

  setValidUser();

  useEffect(() => {
    document.cookie = `UserID=${user.ID}; Max-Age=86400`;
    document.cookie = `UserKey=${user.UserKey}; Max-Age=86400`;
  });
  
  return (
    <main >
      <div className="flex">
        <ChatBox user={user} />
      </div>
    </main>
  );
}