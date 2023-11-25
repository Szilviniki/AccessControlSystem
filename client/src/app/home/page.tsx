'use client'
import {Card, Col, Row} from "react-bootstrap";
import React from "react";
import HomePLayout from "@/app/home/HomePLayout";

export default function HomePage() {
    return (
        <>
            <HomePLayout>
                <Row>
                    <Col>
                        <Card className="CardHP" id="present">
                            <h1>jelen</h1>
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
            </HomePLayout>
        </>
    );
}
