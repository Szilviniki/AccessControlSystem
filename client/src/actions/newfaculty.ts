"use server"

import {IResponse} from "@/interfaces/Response";

export async function saveFaculty(formData:FormData): Promise<IResponse>{
    try {
        const body = JSON.stringify({
            name: formData.get("name"),
            email: formData.get("email"),
            phone: formData.get("phone"),
            role: formData.get("role"),
        });

        const res = await fetch('http://localhost:4001/api/v1/Faculty/Add',{
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