import {Card, Col, Container, Row} from "react-bootstrap";
import TopNavigation from "@/components/Navbar/TopNavigation";
import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import '../style/globals.css'
import SideMenu from "../../components/Navbar/Sidebar";

export default function Template({ children }: { children: React.ReactNode }) {

    return (
        <Container fluid className='p-0 overflow-hidden'>
            <Row className=" justify-content-center fluid" style={{height: "100vh"}}>
                <Col sm={6} md={8} lg={7} className="align-items-center d-flex" style={{height: '100vh'}}>
                    <Card className="loginC w-100  justify-content-center ">
                        {children}
                    </Card>
                </Col>
            </Row>
        </Container>
    )
}
