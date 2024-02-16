/**"use server"

type NewPasswordResponse = {
    error?: boolean
    messages? : string[]|string
}
export async function newpassword(formData:FormData): Promise<NewPasswordResponse>{

    try {
        const res = await fetch(`http://localhost:4001/api/v1/Post/Faculty`,{

            method: "POST",
            headers:{
                "Content-Type": "application/json"
            },
            body:JSON.stringify({Password: formData.get("email"))

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
}**/