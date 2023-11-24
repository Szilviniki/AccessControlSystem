import express from "express";
import cors from "cors";
import login from "./login/routes/login";

const app = express()
const port = 4001;
app.use(express.json())
app.use(express.urlencoded(({
        extended: true
    }))
)
app.use(cors())
app.use("/login",login)
app.listen(port, ()=>{
    console.log(`app listening on ${port}`)
})