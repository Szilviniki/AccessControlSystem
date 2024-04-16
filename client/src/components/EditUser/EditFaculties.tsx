'use client'
import React, {useEffect, useState} from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import {MdEdit} from "react-icons/md";
import {Col, Container, Form, Row} from "react-bootstrap";

function EditFacultiesForm(props: any ,id:string) {
    const [student, setData] = useState([]);

    useEffect(() => {
        fetch(`http://localhost:4001/api/v1/Faculty/Get/${props.id}`).then((res) => {
            res.json().then((datas) => {
                setData(datas.data)
            })
        })

    }, [])

    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            scrollable
            className="w-100"
        >
            <Modal.Header>
                <Modal.Title id="contained-modal-title-vcenter">
                    Új diák hozzáadása
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Container className="justify-content-center">
                    <form>
                        <Row className="justify-content-center">
                            <Col className="justify-content-center">
                                <h2 className="m-4">Diák adatai</h2>
                                <Form.Group>
                                    <Form.Control
                                        // value={student.name}
                                        type="text"
                                        name="name"
                                        placeholder="Dolgozó neve"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        type="email"
                                        name="email"
                                        placeholder="Dolgozó email cím"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        type="text"
                                        name="phone"
                                        placeholder="Dolgozó telefonszám"
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
export default function EditFaculties(props: any, id:string) {
    const [modalShow, setModalShow] = React.useState(false);

    return (
        <>
            <Button className="buttonedit" onClick={() => setModalShow(true)}><MdEdit /></Button>
            <EditFacultiesForm
                show={modalShow}
                onHide={() => setModalShow(false)}
                id={id}
            />
        </>
    );
}