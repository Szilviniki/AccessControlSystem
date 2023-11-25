import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../globals.css';
import {Col, Container, Image, Nav, Row} from 'react-bootstrap';
import React from 'react';
import Navbar from 'react-bootstrap/Navbar';
import kep from '@/../public/images/hous.svg';

const inter = Inter({ subsets: ['latin'] });

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
                <Col sm={2} style={{ height: '100vh' }} id="Nav">
                    <Image src="../../public/images/hous.svg" alt="itt lenne a kép"/>
                    <Navbar className="bg-body-tertiary">
                        <Container>
                            <Navbar.Brand >Band linkr</Navbar.Brand>
                        </Container>
                    </Navbar>
                    <Navbar className="bg-body-tertiary">
                        <Container>
                            <Navbar.Brand className="navItem" >Band linkr</Navbar.Brand>
                        </Container>
                    </Navbar>
                </Col>

                <Col>
                    <Navbar className="bg-body-tertiary">
                        <Container>
                            <Navbar.Text>Tűzeset</Navbar.Text>
                            <Navbar.Toggle />
                            <Navbar.Collapse className="justify-content-end">
                                <Navbar.Text>Jó napot! Felhasználó</Navbar.Text>
                            </Navbar.Collapse>
                        </Container>
                    </Navbar>
                    <Container>
                        <Row>
                            <Col>{children}</Col>
                        </Row>
                    </Container>
                </Col>
            </Row>
        </>
    );
}
