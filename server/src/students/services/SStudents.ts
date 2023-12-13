import {IStudent} from "../../interfaces/People";
import {PrismaClient} from "@prisma/client";

const prisma = new PrismaClient

async function AddStudent(studentData: IStudent) {
    try {
        await prisma.students.create({
            data: {
                id:studentData.id,
                name: studentData.name,
                age: studentData.age,
                is_in: JSON.parse(String(studentData.is_in)),
                group_id: studentData.group_id,
                email: studentData.email,
                phone: studentData.phone
            }
        })
        return {success: true, comment: studentData}
    } catch (e) {
        console.log(e)
        return {success: false, comment: e}
    }
}

async function RemoveStudent(student_id:string){
    console.log(student_id)
    try {
       const deleted = await prisma.students.delete({where:{id:student_id}})
        return {success:true, message:deleted}
    }catch (e){
        console.log(e)
        return {success:false, message:e}
    }
}

async function GetAllStudents(){
    try {
        const students = await prisma.students.findMany()
        return({data:students, success:true})
    }catch (e){
        return {data:e, success:true}
    }
}

async function GetStudent(student_id:string) {
    try {
        const result =  await prisma.students.findFirst({where:{
            id:student_id
            }})
        if (result===null){
            return {success:false, message:"no student found"}
        }
        return {success:true, message:result}
    }catch (e) {
        return {success:false, message:e}
    }
}

export {AddStudent, RemoveStudent, GetAllStudents, GetStudent}