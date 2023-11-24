import bcrypt from "bcrypt";

async function HashPassword(password:string){
    return await bcrypt.hash(password, 9)
}

async function ComparePassword(plain:string, hash:string){
    return bcrypt.compare(plain, hash)
}

export {HashPassword,ComparePassword}