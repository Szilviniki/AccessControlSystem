'use client'
import {Nav} from "react-bootstrap";
import React from "react";

export default function NavItemStudents() {
    return(
        <Nav.Link eventKey='/notes' href="/mail">
            <p className="NavLink"><img src='/images/mailbox.svg' alt="Üzenetek kép"/> Üzenetek</p>
        </Nav.Link>


)
}