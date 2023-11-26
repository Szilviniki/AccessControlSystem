import {PrismaClient} from "@prisma/client"
import {ComparePassword} from "../../functions/password_functions";

const prisma = new PrismaClient

async function loginCheck(user: string, password: string) {
    try {
        const a = await prisma.faculty.findUniqueOrThrow({
            where: {
                username: user
            }
        })
        if (await ComparePassword(password, a.password)) {
            return {successful: true}
        } else {
            return {successful: false, message: "mismatch"}
        }
    } catch (e) {
        return {successful: false, message: e}
    }

}

async function dumbLoginCheck(user: string, pwd: string) {
    try {
        const a = await prisma.faculty.findFirstOrThrow({
            where: {
                username: user
            }
        })
        if (pwd === a.password) {
            return {successful: true}
        } else {
            return {successful: false, message: "mismatch"}
        }
    } catch (e) {
        console.log(e)
        return {successful: false, message: e}
    }
}

export {loginCheck, dumbLoginCheck}