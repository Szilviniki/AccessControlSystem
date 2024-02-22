"use server"

type CheckResponse = {
    error?: boolean
    messages? : string[]|string
}
export async function check(formData:FormData): Promise<CheckResponse>{
    try {
        const res = await fetch(`http://localhost:4001/api/v1/CheckIn/Faculty`,{

            method: "POST",
            headers:{
                "Content-Type": "application/json"
            },
            body:JSON.stringify({Code:formData.get("code")})


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