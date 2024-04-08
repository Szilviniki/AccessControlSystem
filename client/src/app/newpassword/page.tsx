'use client'

import Template from "@/app/templates/Template";
import {Col, Row} from "react-bootstrap";
import React from "react";
import NewPasswordForm from "@/components/NewPassword/NewPasswordForm";
import {FaKey} from "react-icons/fa";

export default function NewPassword() {
    return (
        <Template>
            <Row className=" justify-content-center ">
                <Col sm={6} md={12} lg={6}>
                    <FaKey className="key" size={150}/>
                </Col>
            </Row>
            <Row className=" justify-content-center ">
                <Col sm={5} md={5}>
                    <NewPasswordForm/>
                </Col>
            </Row>
        </Template>
    );
}

