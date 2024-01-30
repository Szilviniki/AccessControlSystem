import type {Metadata} from 'next'
import {Inter} from 'next/font/google'
import 'bootstrap/dist/css/bootstrap.min.css';
import '../globals.css'
import {Card, Col, Container, Row} from 'react-bootstrap'
import React from "react";
import Navbar from "react-bootstrap/Navbar";
import {CookiesProvider} from "next-client-cookies/server";
import VerticalNavigation from "@/components/VerticalNavigation";
import TopNavigation from "@/components/TopNavigation";

const inter = Inter({subsets: ['latin']})

export const metadata: Metadata = {
    title: 'Login',
}

export default function LoginLayout({
                                        children,
                                    }: {
    children: React.ReactNode
}) {
    return (
        <Row className=" justify-content-center ">
            <Col sm={8} md={6} lg={8} className="align-items-center d-flex" style={{height: '100vh'}}>
                <Card className="loginC w-100  justify-content-center ">
                    {children}
                </Card>
            </Col>
        </Row>
    )
}
