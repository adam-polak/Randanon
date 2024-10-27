import { RandanonApi, User } from "./definitions";

const apiUrl = `${RandanonApi}/user`;

export async function ValidateUser(user: User) : Promise<boolean> {
    const json = JSON.stringify(user);
    const requestUrl = `${apiUrl}/validuser`;

    const response = await fetch(requestUrl, {
        method: "POST",
        body: json
    });

    const result = await response.text();

    if(result.length == 0) return true;
    else return false;
}

export async function CreateUser() : Promise<User | null> {
    const requestUrl = `${apiUrl}/createuser`;
    const response = await fetch(requestUrl);

    const result = await response.text();

    try {
        const user = JSON.parse(result);
        return user;
    } catch {
        return null;
    }
}