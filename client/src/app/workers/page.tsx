import React, {use} from 'react';
import MainTemplate from '@/app/templates/MainTemplate';
import {Card, Col, Container, Row} from 'react-bootstrap';
import Faculities from "@/components/DataTable/Faculities";
import AddNewWorkersForm from "@/components/NewUser/AddNewWorkersForm";

export default function WorkersPage() {


    return (
    <MainTemplate>
        <Container>
            <Row>
                <Col className="m-2 ">
                    <AddNewWorkersForm/>
                </Col>
            </Row>
            <Row>
                <Col>
                    <Card className="table w-100">
                        <Faculities/>
                    </Card>
                </Col>
            </Row>
        </Container>
    </MainTemplate>
    );
}
