'use client'
import {Card, Col, Nav, NavbarText, NavLink, Row} from "react-bootstrap";
import React from "react";
import HomePLayout from "@/app/home/HomePLayout";
import {getCookie, setCookie} from "cookies-next";
import VerticalNavigation from "@/components/VerticalNavigation";
import Navbar from "react-bootstrap/Navbar";
import {cookies} from "next/headers";

export default function HomePage() {
const user = getCookie('user');
    return (
        <>
            <HomePLayout>
               <Row>
                <Col md={2}>
                    <VerticalNavigation/>
                </Col>
                <Col md={10}>
                    <Row>
                    <Navbar className="fixed-top Top-Nav" >
                        <Navbar.Collapse className="justify-content-end ">
                        <NavbarText className="mx-3" >Jó napot! {user}</NavbarText>
                        </Navbar.Collapse>
                    </Navbar>


                </Row>
                <Row className="margin">
                    <Col>
                        <Card className="CardHP" id="present">
                            <h1>jelen</h1>
                        </Card>
                    </Col>
                    <Col>
                        <Card className="CardHP" id="not-present">
                            <h1>jelenleg nincs bent</h1>
                        </Card>
                    </Col>
                    <Col>
                        <Card className="CardHP" id="problematic">
                            <h1>Problémás diákok</h1>
                        </Card>
                    </Col>
                </Row>

                <Row>
                    <Col>
                        <Col>
                            <Card className="CardHP">
                                <h1>Legutóbbi ki és belépések</h1>
                            </Card>
                        </Col>
                    </Col>
                    <Col>
                        <Col>
                            <Card className="CardHP">
                                <h1>16:00 után kint tartozkodhat</h1>
                            </Card>
                        </Col>
                    </Col>
                </Row>
                </Col>
               </Row>
            </HomePLayout>
        </>
    );
}
