import express, {Request, Response} from "express";
import {IStudent} from "../../interfaces/People";
import {AddStudent, RemoveStudent} from "../services/SStudents";

const students = express.Router()

students.get("/", (res:Response)=>{
    res.status(200)
})

students.post("/add", async (req:Request, res:Response)=>{
    const studentData:IStudent = {
        name:req.body.name,
        group_id:parseInt(req.body.group_id),
        age:parseInt(req.body.age),
        is_in:req.body.is_in,
        email:req.body.email,
        phone:req.body.phone
    }
const result = await AddStudent(studentData)
    if(result.success){
        res.status(200).send(result.created)
    }else{
        res.status(400).send(result.error)
    }
})

students.delete("/delete", async (req:Request, res:Response)=>{
    const result = await RemoveStudent(parseInt(req.body.student_id))
    if (result.success){
        res.status(200).send(result)
    }else {
        res.status(400).send(result)
    }
})

export default students