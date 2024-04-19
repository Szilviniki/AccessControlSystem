'use client'
import React from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import {FaPlusCircle} from "react-icons/fa";
import {save} from "@/actions/NewStudentAction";
import {Container, Form, Row} from "react-bootstrap";
import {useCookies} from "next-client-cookies";
import {saveFaculty} from "@/actions/newfaculty";

function AddNewWorkersForm(props:any) {
    const token = useCookies().get('user-token');
    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            scrollable
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Új Dolgozó felvétele
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Container className="justify-content-center">
                    <Row>

                <form action={saveFaculty}>
                    <input type="hidden" name={"token"} value={token}/>
                                <Form.Control
                                    type="text"
                                    name="name"
                                    placeholder="Dolgozó neve"
                                    className="inputFc m-4"
                                />
                    <Form.Group>
                        <Form.Control
                            type="text"
                            name="email"
                            placeholder="Dolgozó email címe"
                            className="inputFc m-4"
                        />
                    </Form.Group>
                    <Form.Group>
                        <Form.Control
                            type="text"
                            name="phone"
                            placeholder="Dolgozó telefonszáma (+36301234567)"
                            className="inputFc m-4"
                            maxLength={12}
                            minLength={12}
                        />
                    </Form.Group>
                    <Form.Group>
                        <Form.Select name="role" defaultValue={4} >
                            <option value="1">Admin</option>
                            <option value="2">Kollégium vezető</option>
                            <option value="3">Nevelő</option>
                            <option value="4">Technikai dolgozó</option>
                        </Form.Select>
                    </Form.Group>
                            <Form.Group>
                                <Button type="submit" className="mt-2 loginBt"
                                        onClick={props.onHide}>Mentés</Button>
                            </Form.Group>
                </form>
                    </Row>
                </Container>
            </Modal.Body>

        </Modal>
    );
}

export default function AddNewWorkers() {
    const [modalShow, setModalShow] = React.useState(false);

    return (
        <>
            <Button className="_new" onClick={() => setModalShow(true)}>
                <FaPlusCircle className="mx-1 mb-1"/>
                Új dolgozó
            </Button>

            <AddNewWorkersForm
                show={modalShow}
                onHide={() => setModalShow(false)}
            />
        </>
    );
}
