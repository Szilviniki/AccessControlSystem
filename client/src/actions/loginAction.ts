"use server"

import {ILoginResponse} from "@/interfaces/LoginResponse";

export async function login(formData:FormData): Promise<ILoginResponse>{
    try {
        const res = await fetch(`http://localhost:4001/api/v1/Auth/Check`,{

            method: "POST",
            headers:{
                "Content-Type": "application/json"
            },
            body:JSON.stringify({Email: formData.get("email"),Password: formData.get("password")})


        })

        const data = await res.json();


        return data

    } catch (e){

       return {
            queryIsSuccess: false,
            message: "Sikertelen bejelentkezés!"

       }
    }
}