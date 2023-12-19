import type {Metadata} from 'next';
import {Inter} from 'next/font/google';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../globals.css';
import {Col, Container, Nav, Row} from 'react-bootstrap';
import React from 'react';
import NavItemStudents from "@/components/NavItemStudents";
import NavItemNotes from "@/components/NavItemNotes";
import NavItemMail from "@/components/NavItemMail";



const inter = Inter({subsets: ['latin']});

export const metadata: Metadata = {
    title: 'Home',
};

export default function HomePLayout({
                                        children,
                                    }: {
    children: React.ReactNode;
}) {
    return (
        <>
            <Row>
                <Col md={2} className="Vertical-Nav">
                    <Nav defaultActiveKey="/home" className="flex-column">
                       <NavItemStudents/>
                        <NavItemMail/>
                        <NavItemNotes/>

                    </Nav>
                </Col>
                <Col md={10}>
                    <Row className="Nav">
                        <Col sm={{span: 3, offset: 2}}>
                            <h2>Tűz</h2>
                        </Col>
                        <Col sm={{span: 6, offset: 1}}>
                            <h2>Jó napot!</h2>
                        </Col>
                    </Row>
                    <Container>
                        <Row>
                            <Col sm={12}>{children}</Col>
                        </Row>
                    </Container>
                </Col>
            </Row>


        </>
);
}
