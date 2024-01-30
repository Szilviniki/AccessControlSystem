'use client'
import NavItemStudents from "@/components/NavigationItem/NavItemStudents";
import NavItemMail from "@/components/NavigationItem/NavItemMail";
import NavItemNotes from "@/components/NavigationItem/NavItemNotes";
import {Col, Nav, Row} from "react-bootstrap";
import NavItemSettings from "@/components/NavigationItem/NavItemSettings";
import NavItemExit from "@/components/NavigationItem/NavItemExit";
import NavItemHome from "@/components/NavigationItem/NavItemHome";
import React from "react";

export default function VerticalNavigation(   ) {
    const pages =["NavItemHome","NavItemStudents","NavItemMail","NavItemNotes","NavItemSettings"]
    return(

        <Row>
            <Col md={12}>
        <Nav defaultActiveKey="/home" className="flex-column position-fixed Vertical-Nav" >
            <img src="/images/house-lock.svg" className="Nav-Img justify-content-center" alt="logó"/>
            <Row className="m-2">
            <NavItemHome/>
            <NavItemStudents/>
            <NavItemMail/>
            <NavItemNotes/>
            </Row>
            <Row className=" mt-20">
                <Col md={6}>
                    <NavItemSettings/>
                </Col>
                <Col md={6} >
                    <NavItemExit/>
                </Col>
            </Row>

        </Nav>
        </Col>
        </Row>
    )
}
