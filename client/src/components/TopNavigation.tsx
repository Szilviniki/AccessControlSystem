'use client'
import Navbar from "react-bootstrap/Navbar";
import {NavbarText} from "react-bootstrap";
import React from "react";


export default function TopNavigation() {
   // const email = cookies().get("email");
    return(
        <Navbar className="fixed-top Top-Nav" >
            <Navbar.Collapse className="justify-content-end ">
                <NavbarText className="mx-3" >Jó napot!</NavbarText>
            </Navbar.Collapse>
        </Navbar>
    )
}