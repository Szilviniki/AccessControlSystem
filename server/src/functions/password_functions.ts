import bcrypt from "bcrypt";

async function HashPassword(password:string){
    return await bcrypt.hash(password, 9)
}

export {HashPassword}