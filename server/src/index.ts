import express from "express";
import cors from "cors";
import login from "./login/routes/RLogin";
import students from "./students/routes/RStudents";
import gate_logs from "./gate logs/routes";
import {PrismaClient} from "@prisma/client";
import * as timers from "timers";

async function attendance() {
    const p = new PrismaClient()
    const date = new Date()
    console.log("students")
    const student_ids = await p.students.findMany({select: {id: true}})
    for (const value of student_ids) {
        const student_results = await p.gate_logs.create({
            data: {
                person_id: parseInt(value.id),
                timestamp: date.toLocaleString()
            }
        })
        console.log(student_results)
    }
    console.log("faculty")
    const faculty_ids = await p.faculty.findMany({select: {id: true}})
    for (const value of faculty_ids) {
        const faculty_results = await p.gate_logs.create({
            data: {
                person_id: value.id,
                timestamp: date.toLocaleString()
            }
        })
        console.log(faculty_results)
    }
    if (date.getDay() === 5 && date.getHours() > 16) {


        //     Changest status of everyone to absent
        const students_left = await p.students.updateMany({data: {is_in: false}, where: {is_in: true}})
        const faculty_left = await p.faculty.updateMany({data: {is_in: false}, where: {is_in: true}})
        console.log(`${JSON.stringify(faculty_left)},${JSON.stringify(students_left)}`)
    }
}

const app = express()
const port = 4001;
app.use(express.json())
app.use(express.urlencoded(({
        extended: true
    }))
)
app.use(cors())
app.use("/login", login)
app.use("/students", students)
app.use("/logs", gate_logs)
app.listen(port, async () => {
    await attendance()
    timers.setInterval(async () => {
        await attendance()
    }, 3600000)
    console.log(`app listening on ${port}`)
})