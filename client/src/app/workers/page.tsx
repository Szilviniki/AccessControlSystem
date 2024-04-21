import React from 'react';
import MainTemplate from '@/app/templates/MainTemplate';
import {Card, Col, Container, Row} from 'react-bootstrap';
import Faculties from "@/components/DataTable/Faculties";
import AddNewWorkersForm from "@/components/NewUser/AddNewWorkersForm";
import {redirect} from "next/navigation";
import {cookies} from "next/headers";



export default function WorkersPage() {
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
                            {(role=="1") && (
                            <AddNewWorkersForm/>
                                )}
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <Card className="table w-100">
                                <Faculties/>
                            </Card>
                        </Col>
                    </Row>
                </Container>
            </MainTemplate>
        );
    }
}
