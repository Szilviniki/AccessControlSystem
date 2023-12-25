'use client'
import NavItemStudents from "@/components/NavItemStudents";
import NavItemMail from "@/components/NavItemMail";
import NavItemNotes from "@/components/NavItemNotes";
import {Col, Nav, Row} from "react-bootstrap";
import NavItemSettings from "@/components/NavItemSettings";
import NavItemExit from "@/components/NavItemExit";
import NavItemHome from "@/components/NavItemHome";
import React from "react";

export default function VerticalNavigation() {
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