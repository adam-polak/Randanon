'use client'

import { useEffect, useState } from "react";
import { validationScript } from "./lib/util/scripts";
import getUserFromCookies from "./lib/cookies";
import ChatBox from "./lib/ui/chat";
import { User } from "./lib/util/definitions";

export default function Home() {

  let [user, setUser] = useState<User>(
    {
      ID: 0,
      UserKey: 0
    }
  )

  useEffect(() => {

    const setUserFromCookies = () => {
      if(user.ID == 0 || user.UserKey == 0) {
        user = getUserFromCookies(document.cookie);
        setUser(user);
      }
    }

    const validUser = async () => {
      const changeUser = await validationScript(user);
      if(changeUser.UserKey != user.UserKey) {
        setUser(changeUser);
      }
    }

    setUserFromCookies();
    validUser();
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