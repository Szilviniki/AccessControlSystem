"use server"

type CheckResponse = {
    data?: null
    messages? : string[]|string
    queryIsSuccess?:boolean
}
export async function checkStudent(formData:FormData): Promise<CheckResponse>{
    try {
        const res = await fetch(`http://localhost:4001/api/v1/CheckIn/CheckStudent`,{

            method: "POST",
            headers:{
                "Content-Type": "application/json"
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