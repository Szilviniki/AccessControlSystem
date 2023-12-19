'use client'
import {Nav} from "react-bootstrap";
import React from "react";

export default function NavItemStudents() {
    return(
        <Nav.Link eventKey='/notes' className="Nav-Item">
            <img src='/images/pencil.svg' alt="Feljegyzések kép"/> Feljegyzések
        </Nav.Link>


    )
}