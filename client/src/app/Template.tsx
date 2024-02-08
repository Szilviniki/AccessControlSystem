import {Card, Col, Container, Row} from "react-bootstrap";
import TopNavigation from "@/components/TopNavigation";
import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import './globals.css'
import SideMenu from "../components/Sidebar";

export default function Template({ children }: { children: React.ReactNode }) {
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
