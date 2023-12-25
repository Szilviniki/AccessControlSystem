'use client'
import {Nav} from "react-bootstrap";
import React from "react";

export default function NavItemStudents() {
    return(
        <Nav.Link eventKey="/student" className="Nav-Item">
            <p className="NavLink"><img src='/images/people.svg' alt="Diákok kép"/> Diákok</p>
        </Nav.Link>


)
}