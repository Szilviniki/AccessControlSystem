"use server"

type LoginResponse = {
    error?: boolean
    messages? : string[]|string
}
export async function login(formData:FormData): Promise<LoginResponse>{
    try {
        const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/employees/login`,{
            method: "POST",
            headers:{
                "Content-Type": "application/jsom"
            },
            body: JSON.stringify({
                email: formData.get("email"),
                password: formData.get("password")
            })
        })

        const data = await res.json();
        return data
    } catch (e){
       return {
            error: true,
            messages: "Sikertelen bejelentkezés!"
        }
    }
}