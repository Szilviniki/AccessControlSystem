'use client'
import {Nav} from "react-bootstrap";
import React from "react";

export default function NavItemStudents() {
    return(
        <Nav.Link eventKey="/" className="Nav-Item">
            <img src='/images/box-arrow.svg' alt="Kilépés kép"  />
        </Nav.Link>


    )
}