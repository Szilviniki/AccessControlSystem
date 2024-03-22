import React from "react";
import {cookies} from "next/headers";
import {redirect} from "next/navigation";
import MainTemplate from "@/app/templates/MainTemplate";


export default function NotesPage() {
    const user = cookies().get("user");

    if (!user) {
        redirect("/login")
    }
    else {

        return(

            <>

                <MainTemplate>
                    notes
                </MainTemplate>
            </>
        )}
}
