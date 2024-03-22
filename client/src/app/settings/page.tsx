import {Card, Col, Row} from "react-bootstrap";
import React from "react";
import {getCookies} from "next-client-cookies/server";
import MainTemplate from "@/app/templates/MainTemplate";
import {cookies} from "next/headers";
import {redirect} from "next/navigation";
import {useCookies} from "next-client-cookies";


export default function SettingsPage() {
    const user = cookies().get("user");

    if (!email) {
        redirect("/user")
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
