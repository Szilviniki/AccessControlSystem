'use client'
import {Nav} from "react-bootstrap";
import React from "react";

export default function NavItemStudents() {
    return(
        <Nav.Link eventKey='/home' className="Nav-Item">
            <img src='/images/hous.svg' alt="Kezdőlap kép"  /> Kezdőlap
        </Nav.Link>


    )
}