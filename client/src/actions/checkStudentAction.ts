"use server"
import jwt from "jsonwebtoken"

type CheckResponse = {
    data?: null
    messages? : string[]|string
    queryIsSuccess?:boolean
}
export async function checkStudent(formData:FormData): Promise<CheckResponse>{
    try {
        const token = jwt.sign(
            {
                "apiKey": process.env.NEXT_PUBLIC_API_KEY as string
            },
            process.env.NEXT_PUBLIC_JWT_KEY as string,
            {
                expiresIn: "5m"
            }
        );
        const res = await fetch(`http://localhost:4001/api/v1/CheckIn/CheckStudent`,{

            method: "POST",
            headers:{
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            cache:"no-cache",
            body:formData.get("code")


        })

        const data = await res.json();
        console.log(data)
        return data

    } catch (e){
        console.log(e);
        return {
            queryIsSuccess: false,
            messages: "Sikertelen!"

        }
    }
}