"use server"

type LoginResponse = {
    email? : string[]|string
    name? : string[]|string
    role? : string[]|string
    token? : string[]|string
    error?: boolean
    data?: null
    message? : string[]|string
    queryIsSuccess? : boolean
}
export async function login(formData:FormData): Promise<LoginResponse>{
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
        console.log(e);
       return {
            queryIsSuccess: false,
            message: "Sikertelen bejelentkezés!"

       }
    }
}