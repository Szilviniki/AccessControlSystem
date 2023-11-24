import {IStudent} from "../../interfaces/People";
import {PrismaClient} from "@prisma/client";

const prisma = new PrismaClient

async function AddStudent(studentData: IStudent) {
    try {
        await prisma.students.create({
            data: {
                name: studentData.name,
                age: studentData.age,
                is_in: JSON.parse(String(studentData.is_in)),
                group_id: studentData.group_id,
                email: studentData.email,
                phone: studentData.phone
            }
        })
        return {success: true, created: studentData}
    } catch (e) {
        console.log(e)
        return {success: false, error: e}
    }
}

async function RemoveStudent(student_id:number){
    console.log(student_id)
    try {
       const deleted = await prisma.students.delete({where:{id:student_id}})
        return {success:true, deleted:deleted}
    }catch (e){
        console.log(e)
        return {success:false, error:e}
    }
}

export {AddStudent, RemoveStudent}