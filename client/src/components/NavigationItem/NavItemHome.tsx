'use client'
import {Nav} from "react-bootstrap";
import React from "react";

export default function NavItemHome() {
    return(
        <Nav.Link eventKey='/' href="/">
            <p className="NavLink"><img src='/images/hous.svg' alt="Kezdőlap kép"/> Kezdőlap</p>
        </Nav.Link>


    )
}