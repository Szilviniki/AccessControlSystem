"use server"

import {IResponse} from "@/interfaces/Response";

export async function save(formData:FormData): Promise<IResponse>{
    try {
        const body = JSON.stringify({
            studentName: formData.get("name"),
            studentEmail: formData.get("email"),
            studentPhone: formData.get("phone"),
            studentBirthDate: new Date(formData.get("birthday") as string),
            parentName: formData.get("parentname"),
            parentEmail: formData.get("parentemail"),
            parentPhone: formData.get("parentphone"),
        });
        const res = await fetch('http://localhost:4001/api/v1/Students/AddWithParent',{
            method: "PUT",
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