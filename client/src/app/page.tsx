
import {Card, Col, Row} from "react-bootstrap";
import React from "react";
import {getCookies} from "next-client-cookies/server";
import MainTemplate from "@/app/templates/MainTemplate";
import {cookies} from "next/headers";
import {redirect} from "next/navigation";
import {useCookies} from "next-client-cookies";


export default function HomePage() {

    const user = cookies().get("user");
console.log(user)
    if (!user) {
        redirect("/login")
    }
    else {

    return(

        <>

        <MainTemplate>
                <Row>
                    <Col>
                        <Card className="CardHP" id="present">
                            <h1></h1>

                        </Card>
                    </Col>
                    <Col>
                        <Card className="CardHP" id="not-present">
                            <h1>jelenleg nincs bent</h1>
                        </Card>
                    </Col>
                    <Col>
                        <Card className="CardHP" id="problematic">
                            <h1>Problémás diákok</h1>
                        </Card>
                    </Col>
                </Row>

                <Row>
                    <Col>
                        <Col>
                            <Card className="CardHP">
                                <h1>Legutóbbi ki és belépések</h1>
                            </Card>
                        </Col>
                    </Col>
                    <Col>
                        <Col>
                            <Card className="CardHP">
                                <h1>16:00 után kint tartozkodhat</h1>
                            </Card>
                        </Col>
                    </Col>
                </Row>
        </MainTemplate>
        </>
    )}
}
