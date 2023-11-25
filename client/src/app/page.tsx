"use client";
import Form from "@/components/Form";
import {Row, Col, Card, Image} from "react-bootstrap";
import React from "react";
import LoginLayout from "@/app/LoginLayout";

export default function Home() {
    return (
        <LoginLayout>
            <Row className=" justify-content-center ">
                <Col
                    sm={6}>
                    <Image src="images/person-circle.svg"
                           alt="itt lenne a kép"
                           id="loginImage"
                    />
                </Col>
            </Row>
            <Row className=" justify-content-center ">
                <Col sm={5}>
                    <Form
                        inputs={[
                            {
                                id: "username",
                                label: "Email cím",
                                type: "text"
                            }, {
                                id: "password",
                                label: "Jelszó",
                                type: "password",
                            }, {
                                id: "Submit",
                                label: "Belépés",
                                type: "button",
                            }

                        ]}
                        onSubmitFunction={(values) => {
                            console.log(values)
                        }}
                    />
                </Col>
            </Row>
        </LoginLayout>
    )
}
