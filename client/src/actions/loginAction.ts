"use server"
import {cookies} from "next/headers";

type LoginResponse = {
    error?: boolean
    messages? : string[]|string
}
export async function login(formData:FormData): Promise<LoginResponse>{
    try {
        const res = await fetch(`http://localhost:4001/api/v1/Login/TestLogin`,{

            method: "POST",
            headers:{
                "Content-Type": "application/json"
            },
            body:JSON.stringify({Email: formData.get("email"),Password: formData.get("password")})

        })



        const data = await res.json();


        return data

    } catch (e){
        console.log(e);
       return {
            error: true,
            messages: "Sikertelen bejelentkezés!"

        }
    }
}