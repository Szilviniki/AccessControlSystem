'use client'
import {Nav} from "react-bootstrap";
import React from "react";
import {deleteCookie} from "cookies-next";

export default function NavItemExit() {
    deleteCookie("email" )
    return(
        <Nav.Link eventKey="/" href="/" className="Nav-Item" >
            <img src='/images/box-arrow.svg' alt="Kilépés kép" />

        </Nav.Link>



    )
}