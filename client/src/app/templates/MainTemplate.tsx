import {Container} from "react-bootstrap";
import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import '../style/globals.css'
import Content from "@/components/Content";

export default function MainTemplate({ children }: { children: React.ReactNode }) {

    return (
        <Container fluid className='p-0 overflow-hidden'>
            <div className={"d-flex p-0 overflow-hidden position-relative"} style={{height: "100vh"}}>
                <Content>
                    {children}
                </Content>
            </div>
        </Container>
    )
}
