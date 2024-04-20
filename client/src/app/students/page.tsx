import MainTemplate from "@/app/templates/MainTemplate";
import Students from "@/components/DataTable/Students";
import {Card, Col, Container, Row} from "react-bootstrap";
import React from "react";
import AddNewStudent from "@/components/NewUser/AddNewStudent";
import {redirect} from "next/navigation";
import {cookies} from "next/headers";


export default function StudentPage() {

    const user = cookies().get("user-name");
    const role = (cookies().get("user-role") || "") as string;


    if (!user) {
        redirect("/login")
    } else {
        return (
            <MainTemplate>
                <Container>
                    <Row>
                        <Col className="m-2 ">
                            {(role=="1" || role=="2") && (
                                <AddNewStudent/>
                            )}

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
