import express, {Request, Response} from "express";
import {dumbLoginCheck, loginCheck} from "../services/SLogin";
import {HashPassword} from "../../functions/password_functions";

const login = express.Router()

//validates provided RLogin credentials
login.get("/check", async (req: Request, res: Response) => {
    if (req.body.test =="true"){
        const gotBack = await dumbLoginCheck(req.body.username, req.body.password);
        if (gotBack.successful) {
            res.send(JSON.stringify(gotBack)).status(200)
        } else {
            res.send(JSON.stringify(gotBack)).status(400)
        }
    }else{
        const gotBack = await loginCheck(req.body.username, req.body.password);
        if (gotBack.successful) {
            res.send(JSON.stringify(gotBack)).status(200)
        } else {
            res.send(JSON.stringify(gotBack)).status(400)
        }
    }

})

login.get("/hash", async(req:Request, res:Response)=>{
   const h = await HashPassword(req.body.pwd)
    res.send({old:req.body.pwd, hash:h})
})
export default login;
