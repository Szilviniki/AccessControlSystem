"use server"

import {IResponse} from "@/interfaces/Response";

export async function updateUser(formData:FormData): Promise<IResponse>{
    try {
        const body = JSON.stringify({
            name: formData.get("name"),
            email: formData.get("email"),
            phone: formData.get("phone"),
            password: formData.get("password"),
        });

        const res = await fetch('http://localhost:4001/api/v1/Faculty/Update/'+(formData.get("id")as string),{
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

        console.log(data)
        return data

    } catch (e){
        console.log(e);
        return {
            queryIsSuccess: false,
            message: "Sikeres módosítás!"

        }
    }
}