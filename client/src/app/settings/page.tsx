import {Card, Col, Row} from "react-bootstrap";
import React from "react";
import {getCookies} from "next-client-cookies/server";
import MainTemplate from "@/app/templates/MainTemplate";
import {cookies} from "next/headers";
import {redirect} from "next/navigation";
import {useCookies} from "next-client-cookies";


export default function SettingsPage() {
    const email = cookies().get("email");

    if (!email) {
        redirect("/login")
    }
    else {

        return(

            <>

                <MainTemplate>
               settings
                </MainTemplate>
            </>
        )}
}
