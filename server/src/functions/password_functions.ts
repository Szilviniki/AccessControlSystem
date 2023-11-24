import bcrypt from "bcrypt";

async function toHash(password:string){
    return await bcrypt.hash(password, 9)
}

export {toHash}