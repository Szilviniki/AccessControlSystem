"use server"

import {IResponse} from "@/interfaces/Response";

export async function SaveNotes(formData:FormData): Promise<IResponse>{
    try {
        const body = JSON.stringify({
            name: formData.get("name"),
            dayOfWeek: formData.get("dayOfWeek"),
            studentLockRules: formData.get("studentLockRules"),
        });

        const res = await fetch('http://localhost:4001/api/v1/Students/AddWithParent',{
            method: "PUT",
            headers:{
                "Content-Type": "application/json"
            },
            body: body
        })

        const data = await res.json();


        return data

    } catch (e){
        console.log(e);
        return {
            queryIsSuccess: false,
            message: "Sikeres létrehozás!"

        }
    }
}