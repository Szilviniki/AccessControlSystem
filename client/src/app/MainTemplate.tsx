import {Col, Row} from "react-bootstrap";
import VerticalNavigation from "@/components/VerticalNavigation";
import TopNavigation from "@/components/TopNavigation";
import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import './globals.css'

export default function MainTemplate({ children }: { children: React.ReactNode }) {
    return (
        <Row>
            <Col md={2} className="p-0">
                <VerticalNavigation/>
            </Col>
            <Col md={10}>
                <Row>
                    <Col>
                        <TopNavigation/>
                    </Col>
                </Row>
                <Row>
                    <Col className="margin">
                        {children}
                    </Col>
                </Row>
            </Col>
        </Row>
    )
}