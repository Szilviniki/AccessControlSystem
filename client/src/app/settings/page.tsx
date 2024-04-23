import React from "react";
import MainTemplate from "@/app/templates/MainTemplate";
import {cookies} from "next/headers";
import {redirect} from "next/navigation";
import SettingsForm from "@/components/Settings/SettingsForm";
import {Col, Container, Row} from "react-bootstrap";

export default function SettingsPage() {
    const user = cookies().get("user-name");

    if (!user) {
        redirect("/login")
    }
    else {

        return(

            <>

                <MainTemplate>
                    <Container>
                        <Row>
                            <Col>
                                <h2>Adatok módosítása</h2>
                            </Col>
                        </Row>
                        <Row >
                            <Col sm={5} md={12}>
                                 <SettingsForm/>
                            </Col>
                        </Row>
                    </Container>
                </MainTemplate>
            </>
        )
    }
}
