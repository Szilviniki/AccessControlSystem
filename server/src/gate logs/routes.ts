import express, {Response, Request} from "express";
import {ILogs} from "../interfaces/Others"
import {NewLog} from "./services";

const gate_logs = express.Router()


gate_logs.post("/new", async (req: Request, res: Response) => {
    let log:ILogs = {
        person_id: parseInt(req.body.person_id),
        // is_guest: req.body.is_guest,
        is_guest: null,
        timestamp: new Date().toLocaleString()
    }
    // console.log(log)
    let result =await NewLog(log)
    res.json(result)
})


export default gate_logs