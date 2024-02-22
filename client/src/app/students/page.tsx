import MainTemplate from "@/app/templates/MainTemplate";
import Students from "@/components/DataTable/Students";
import {Card, Col, Container, Row} from "react-bootstrap";
import React from "react";
import AddNewStudent from "@/components/NewUser/AddNewStudent";


export default function StudentPage(){
    return (

        <MainTemplate>
            <Container>
            <Row>
                <Col className="m-2 ">
                    <AddNewStudent title="Új diák hozzáadása" content="<AddNewStudentForm/>" />
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

