'use client';

import React, {useState} from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { FaPlusCircle } from "react-icons/fa";
import {Col, Container, Form, Row} from "react-bootstrap";
import {save} from "@/actions/NewStudentAction";

function AddNewStudentForm(props: any) {
    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            scrollable
            className="w-100"
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Új diák hozzáadása
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Container className="justify-content-center">
                    <form action={save}>
                        <Row className="justify-content-center">
                            <Col className="justify-content-center">
                                <h2 className="m-4">Diák adatai</h2>
                                <Form.Group>
                                    <Form.Control
                                        type="text"
                                        name="name"
                                        placeholder="Diák neve"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        type="email"
                                        name="email"
                                        placeholder="Diák email cím"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        type="text"
                                        name="phone"
                                        placeholder="Diák telefonszám"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        type="date"
                                        name="birthday"
                                        placeholder="Diák születési dátum"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                            </Col>
                            <Col className="justify-content-center">
                                <h2 className="m-4">Szülő adatai</h2>
                                <Form.Group>
                                    <Form.Control
                                        type="text"
                                        name="parentname"
                                        placeholder="Szülő neve"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        type="email"
                                        name="parentemail"
                                        placeholder="Szülő email cím"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        type="text"
                                        name="parentphone"
                                        placeholder="Szülő telefonszám"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                            </Col>
                        </Row>
                        <Row className="justify-content-center">
                            <Col>
                                <Form.Group>
                                    <Button type="submit" className="mt-2 loginBt"
                                            onClick={props.onHide}>Mentés</Button>
                                </Form.Group>
                            </Col>
                        </Row>
                    </form>
                </Container>
            </Modal.Body>
        </Modal>
    );
}

export default function AddNewStudent(props: any) {
    const [modalShow, setModalShow] = useState(false);

    return (
        <>
            <Button className="_new" onClick={() => setModalShow(true)}>
                <FaPlusCircle className="mx-1 mb-1"/>
                Új diák
            </Button>

            <AddNewStudentForm
                show={modalShow}
                onHide={() => setModalShow(false)}
                {...props}
            />
        </>
    );
}
