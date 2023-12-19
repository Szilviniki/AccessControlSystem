'use client'
import {Nav} from "react-bootstrap";
import React from "react";

export default function NavItemStudents() {
    return(
        <Nav.Link eventKey="/student" className="Nav-Item">
            <img src='/images/people.svg' alt="Diákok kép" /> Diákok
        </Nav.Link>


    )
}