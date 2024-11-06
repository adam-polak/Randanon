'use client'

import { useEffect, useState } from "react";
import getUserFromCookies from "./lib/util/cookies";
import ChatBox from "./lib/components/chatBox";
import { createUser, validateUser } from "./lib/util/userEvents";
import { User } from "./lib/util/definitions";

export default function Home() {

  const [user, setUser] = useState({ ID: 0, UserKey: 0 });
  const [isUserValidated, setValidated] = useState(false);

  useEffect(() => {
    setInterval(() => {
      if(isUserValidated) {
        document.cookie = `UserID=${user.ID}; Max-Age=86400`;
        document.cookie = `UserKey=${user.UserKey}; Max-Age=86400`;
        return;
      }
  
      const cookieUser = getUserFromCookies(document.cookie);
      setValidUser(cookieUser);
    }, 4000);
  });

  async function setValidUser(user: User) {
    validateUser(user).then( valid => {
      if(!valid) {
        createUser().then(nUser => {
          if(nUser != null) {
            setUser(nUser);
            setValidated(true);
          }
        });
      } else setUser(user);
    });
  }
  
  return (
    <main >
      <div className="flex">
        <ChatBox user={user} />
      </div>
    </main>
  );
}