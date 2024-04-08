import {Card, Col, Container, Row} from "react-bootstrap";
import React from "react";
import MainTemplate from "@/app/templates/MainTemplate";
import {cookies} from "next/headers";
import {redirect} from "next/navigation";
import {FaStar} from "react-icons/fa";
import {RiUserFollowFill, RiUserForbidFill, RiUserUnfollowFill} from "react-icons/ri";
import {PiListFill} from "react-icons/pi";


export default function HomePage() {

    const user = cookies().get("user-name");
console.log(user)
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
                        <Card className="CardHP justify-content-center align-items-center d-flex" id="present" >
                            <RiUserFollowFill  size="3rem" className=""/>
                            <h3>Jelen van</h3>
                            <h2>23</h2>
                        </Card>
                    </Col>
                    <Col>
                        <Card className="CardHP justify-content-center align-items-center d-flex" id="not-present">
                            <RiUserUnfollowFill size="3rem" className=""/>
                            <h3>Igazoltan távol</h3>
                            <h2>23</h2>
                        </Card>
                    </Col>
                    <Col>
                        <Card className="CardHP justify-content-center align-items-center d-flex" id="problematic">
                            <RiUserForbidFill  size="3rem" className=""/>
                            <h3>Igazolás nélkül távol</h3>
                            <h2>23</h2>
                        </Card>
                    </Col>
                </Row>

                <Row>
                    <Col>
                        <Col>
                            <Card className="CardHP">
                                <Row>
                                    <Col md={2}>
                                        <PiListFill size="2rem" className="m-1"/>
                                    </Col>
                                    <Col md={10}>
                                        <h2>Legutóbbi ki és belépések</h2>
                                    </Col>
                                </Row>

                                <h4>gssdv</h4>
                            </Card>
                        </Col>
                    </Col>
                    <Col>
                        <Col>
                            <Card className="CardHP">
                                <Row>
                                    <Col md={2}>
                                        <FaStar size="2rem" className="m-1"/>
                                    </Col>
                                    <Col md={10}>
                                        <h2>16:00 után kint tartózkodhat</h2>
                                    </Col>
                                </Row>

                                <h4 className="justify-content-start">gssdv</h4>
                            </Card>
                        </Col>

                    </Col>
                </Row>
            </Container>
        </MainTemplate>
        </>
    )}
}
