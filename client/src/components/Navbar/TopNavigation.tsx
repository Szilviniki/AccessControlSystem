'use client'
import Navbar from "react-bootstrap/Navbar";
import {NavbarText} from "react-bootstrap";
import React from "react";
import {useCookies} from "next-client-cookies";


export default function TopNavigation() {
const cookies = useCookies();
    return(
        <Navbar className="fixed-top Top-Nav" >
            <Navbar.Collapse className="justify-content-end ">
                <NavbarText className="mx-3" >Jó napot! {useCookies().get("user.name")}</NavbarText>
            </Navbar.Collapse>
        </Navbar>
    )
}