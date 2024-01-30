'use client'
import {Nav} from "react-bootstrap";
import React from "react";

export default function NavItemStudents() {
    return(
        <Nav.Link eventKey="/settings" href="/settings">
            <img src='/images/gear.svg' alt="Beálítások kép" />
        </Nav.Link>


    )
}