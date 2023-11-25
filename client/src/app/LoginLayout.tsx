import type {Metadata} from 'next'
import {Inter} from 'next/font/google'
import 'bootstrap/dist/css/bootstrap.min.css';
import './globals.css'
import {Card, Col, Container, Row} from 'react-bootstrap'
import React from "react";

const inter = Inter({subsets: ['latin']})

export const metadata: Metadata = {
    title: 'Login',
    description: 'Login',
}

export default function LoginLayout({
                                        children,
                                    }: {
    children: React.ReactNode
}) {
    return (
            <Row className=" justify-content-center ">
                <Col sm={8} className="align-items-center d-flex" style={{height: '100vh'}}>
                    <Card className="loginC w-100">
                        {children}
                    </Card>
                </Col>
            </Row>
    )
}
