import MainTemplate from "@/app/templates/MainTemplate";
import Students from "@/components/DataTable/Students";
import {Card, Col, Container, Row} from "react-bootstrap";
import React from "react";
import AddNewStudent from "@/components/NewUser/AddNewStudent";
import {cookies} from "next/headers";
import {redirect} from "next/navigation";
import NewNote from "@/components/NewNote/NewNote";

export default function StudentPage() {
    const user = cookies().get("user-name");

    if (!user) {
        redirect("/user")
    } else {
        return (
            <MainTemplate>
                <Container>
                    <Row>
                        <Col className="m-2 ">
                            <AddNewStudent/>
                        </Col>

                    </Row>
                    <Row>
                        <Col>
                            <Card className="table w-100">
                                <Students/>
                            </Card>
                        </Col>
                    </Row>
                </Container>
            </MainTemplate>
        );
    }
}
