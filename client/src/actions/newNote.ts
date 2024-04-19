"use server"

import {IResponse} from "@/interfaces/Response";

export async function SaveNotes(formData:FormData): Promise<IResponse>{
    try {
        const body = JSON.stringify({
            studentId: formData.get("id"),
            dayOfWeek:formData.get("day"),
            name: formData.get("name"),
        });

        const res = await fetch('http://localhost:4001/api/v1/Students/AddNote',{
            method: "POST",
            headers:{
                "Content-Type": "application/json",
                "Authorization": "Bearer " + (formData.get("token") as string).replaceAll("\"", "").trim(),
                "Access-Control-Allow-Origin": "*",
            },
            mode: "cors",
            body: body
        })

        const data = await res.json();
        return data

    } catch (e){

        return {
            queryIsSuccess: false,
            message: "Sikertelen létrehozás!"

        }
    }
}