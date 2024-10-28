'use client'

import { useEffect, useState } from "react";
import { validationScript } from "./lib/util/scripts";
import getUserFromCookies from "./lib/cookies";

export default function Home() {

  let [user, setUser] = useState(
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
    <main>
      <h1>Hello world!</h1>
      <h1>User: {user.ID} {user.UserKey}</h1>
    </main>
  );
}