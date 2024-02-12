import {Container} from "react-bootstrap";
import TopNavigation from "@/components/Navbar/TopNavigation";
import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import './globals.css'
import SideMenu from "../components/Navbar/Sidebar";
import {cookies} from "next/headers";

export default function MainTemplate({ children }: { children: React.ReactNode }) {

    return (
        <Container fluid className='p-0 overflow-hidden'>
            <div className={"d-flex p-0 overflow-hidden"} style={{height: "100vh"}}>
                <SideMenu />
                <TopNavigation/>

                <div className={"d-flex px-5 py-3 w-100 mt-5 overflow-auto mb-4"}>
                    <h1></h1>
                    {children}
                </div>
            </div>
        </Container>
    )
}
