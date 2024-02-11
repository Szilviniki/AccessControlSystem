import {Col, Container, Row} from "react-bootstrap";
import VerticalNavigation from "@/components/VerticalNavigation";
import TopNavigation from "@/components/TopNavigation";
import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import './globals.css'
import SideMenu from "../components/Sidebar";

export default function MainTemplate({ children }: { children: React.ReactNode }) {
    return (
        <Container fluid className='p-0 overflow-hidden'>
            <div className={"d-flex p-0 overflow-hidden"} style={{height: "100vh"}}>
                <SideMenu />
                <TopNavigation/>
                <div className={"d-flex px-5 py-3 w-100 mt-5 overflow-auto mb-4"}>
                    {children}
                </div>
            </div>
        </Container>
    )
}
