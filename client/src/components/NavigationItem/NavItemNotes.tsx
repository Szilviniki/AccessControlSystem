'use client'
import {Nav} from "react-bootstrap";
import React from "react";

export default function NavItemStudents() {
    return(
        <Nav.Link eventKey='/notes' href="/notes">
            <p className="NavLink"><img src='/images/pencil.svg' alt="Feljegyzések kép"/> Feljegyzések</p>
        </Nav.Link>


)
}