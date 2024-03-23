import React from "react";
import MainTemplate from "@/app/templates/MainTemplate";
import {cookies} from "next/headers";
import {redirect} from "next/navigation";


export default function NotesPage() {
    const user = cookies().get("user-name");

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
