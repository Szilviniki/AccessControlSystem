'use client'
import {Col, Row, Image} from "react-bootstrap";
import {getCookie, setCookie} from "cookies-next";
import LoginForm from "@/components/Login/LoginForm";
import Template from "@/app/Template";

export default function Login() {
    return (
        <Template>
            <Row className=" justify-content-center ">
                <Col sm={6} md={12} lg={6}>
                    <Image src="images/person-circle.svg"
                           alt="itt lenne a kép"
                           className="m-10 h-auto loginImage"/>
                </Col>
                <h1>{getCookie('email')}</h1>
            </Row>
            <Row className=" justify-content-center ">
                <Col sm={5} md={5}>
                    <LoginForm />
                </Col>
            </Row>
        </Template>
);
}
