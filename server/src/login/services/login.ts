import {PrismaClient} from "@prisma/client"
import {toHash} from "../../functions/password_functions";

const prisma = new PrismaClient

interface returns {
    success?:boolean,
    message?:string
}
async function loginCheck(user: string, password: string) {
    try {
        const a = await prisma.faculty.findUniqueOrThrow({
            where: {
                username: user
            }
        })
        return a.password === await toHash(password);

    } catch (e) {
        return false
    }
}

async function dumbLoginCheck(user: string, pwd: string) {
    try {
        const a = await prisma.faculty.findFirstOrThrow({
            where: {
                username: user
            }
        })
        if(pwd===a.password){
            const successful:returns = {
                success:true,
            }
            return successful
        }else{
            const failed:returns={
                success:false,
                message:'bad password'
            }
            return failed
        }
    } catch (e) {
        console.log(e)
        const errorMessage:returns = {
            success:false,
            message:"username not found"
        }
        return errorMessage
    }
}

export {loginCheck, dumbLoginCheck}