import { RandanonApi, User } from "./definitions";

const apiUrl = `${RandanonApi}/user`;

export async function validateUser(user: User) : Promise<boolean> {
    const json = JSON.stringify(user);
    const requestUrl = `${apiUrl}/validuser`;

    const response = await fetch(requestUrl, {
        headers: {
            "Content-Type": "application/json"
        },
        method: "POST",
        body: json,
    });

    const result = await response.text();
    console.log(result);
    if(result.length == 0) return true;
    else return false;
}

export async function createUser() : Promise<User | null> {
    const requestUrl = `${apiUrl}/createuser`;
    const response = await fetch(requestUrl, {
        method: "POST"
    });

    const result = await response.text();

    try {
        const user = JSON.parse(result);
        return user;
    } catch {
        return null;
    }
}