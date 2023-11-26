import express from "express";
import cors from "cors";
import login from "./login/routes/RLogin";
import students from "./students/routes/RStudents";

const app = express()
const port = 4001;
app.use(express.json())
app.use(express.urlencoded(({
        extended: true
    }))
)
app.use(cors())
app.use("/login",login)
app.use("/students",students)
app.listen(port, ()=>{
    console.log(`app listening on ${port}`)
})