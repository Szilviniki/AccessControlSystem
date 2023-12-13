import {ILogs} from "../interfaces/Others"
import {PrismaClient} from "@prisma/client";

const prisma = new PrismaClient()

async function NewLog(log: ILogs) {
    let success: boolean;
    try {
        const result = await prisma.gate_logs.create({
            data: {
                person_id: log.person_id,
                timestamp: log.timestamp,
                is_guest: log.is_guest
            }
        })
        console.log(result)
        success = true
    return ({
        success: success
    })
    } catch (e) {
        success = false;
        return ({
            success: success,
            message: e
        })
    }
}

export {NewLog}

