"use server"

import {ILoginResponse} from "@/interfaces/LoginResponse";

export async function login(formData:FormData): Promise<ILoginResponse>{
    try {
        const res = await fetch(`http://localhost:4001/api/v1/Guardian/update`,{

            method: "POST",
            headers:{
                "Content-Type": "application/json"
            },
            body:JSON.stringify({ Id: formData.get("id"),
                                        Name: formData.get("name"),
                                        Phone: formData.get("phone"),
                                        Email: formData.get("email"),})


        })

        const data = await res.json();


        return data

    } catch (e){
        console.log(e);
        return {
            queryIsSuccess: false,
            message: "Sikertelen módosítás!"

        }
    }
}