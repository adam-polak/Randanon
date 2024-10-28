import { User } from "./definitions";
import { createUser, validateUser } from "./userEvents";

export async function validationScript(user : User) : Promise<User> {
    const isValid = await validateUser(user);
    if(!isValid) return await createUser() ?? user;
    return user;
}