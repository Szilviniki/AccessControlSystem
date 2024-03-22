'use client'
import React from "react";


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
