import type {Metadata} from 'next';
import {Inter} from 'next/font/google';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../globals.css';
import {Col, Row} from 'react-bootstrap';
import React from 'react';

import VerticalNavigation from "@/components/VerticalNavigation";



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
                <VerticalNavigation/>
                <Col>
            <Row>
                {children}
            </Row>
                </Col>
            </Row>


        </>
);
}
