import type {Metadata} from 'next';
import {Inter} from 'next/font/google';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../globals.css';
import {Col, Container, Image, Nav, Row} from 'react-bootstrap';
import React from 'react';
import Navbar from 'react-bootstrap/Navbar';
import kep from '@/../public/images/hous.svg';

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
            <Row style={{background: "red"}}>
                <Col sm={{span: 3, offset: 2}}>
                    <h2>Tűz</h2>
                </Col>
                <Col sm={{span: 6, offset: 2}}>
                    <h2>Jó napot!</h2>
                </Col>
            </Row>
            <Container>
                <Row className="grid-container">
                    <Col sm={12}>{children}</Col>
                </Row>
            </Container>
        </>
);
}
