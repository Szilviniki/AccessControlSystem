import {PrismaClient} from "@prisma/client";
import express, {Request, Response} from "express";
import {dumbLoginCheck, loginCheck} from "../services/login";

const login = express.Router()
const prisma = new PrismaClient

//validates provided login credentials
login.get("/check", async (req: Request, res: Response) => {
    const gotBack = await dumbLoginCheck(req.body.username, req.body.password);
    if (gotBack.success) {
        res.send(JSON.stringify(gotBack)).status(200)
    } else {
        res.send(JSON.stringify(gotBack)).status(400)
    }
})
export default login;
