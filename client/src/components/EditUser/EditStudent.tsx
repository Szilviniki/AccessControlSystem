'use client'

import React, {useEffect, useState} from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import {MdEdit} from "react-icons/md";
import {Col, Container, Form, Row} from "react-bootstrap";
import {StudentProps} from "@/interfaces/Student";
import {useCookies} from "next-client-cookies";

function EditStudentForm(props: any) {
    const [student, setData] = useState([]);
    const cookies = useCookies();
    const token = (cookies.get("user-token") as string);
   console.log(student)

    useEffect(() => {
        fetch(`http://localhost:4001/api/v1/Students/Get/${props.id}`,{
                headers: {
                    "Authorization": "Bearer " + token.replaceAll("\"", "").trim(),
                    "Access-Control-Allow-Origin": "*",
                },
                mode: "cors",
            }
            ).then((res) => {
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
                                        defaultValue={student.name}
                                        type="text"
                                        name="name"
                                        placeholder="Diák neve"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        defaultValue={student.email}
                                        type="email"
                                        name="email"
                                        placeholder="Diák email cím"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        defaultValue={student.phone}
                                        type="text"
                                        name="phone"
                                        placeholder="Diák telefonszám"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                            </Col>
                            <Col className="justify-content-center">
                                <h2 className="m-4">Szülő adatai</h2>
                                <Form.Group>
                                    <Form.Control
                                        defaultValue={student.parent.name}
                                        type="text"
                                        name="parentname"
                                        placeholder="Szülő neve"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        defaultValue={student.parent.email}
                                        type="email"
                                        name="parentemail"
                                        placeholder="Szülő email cím"
                                        className="inputFc m-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        defaultValue={student.parent.phone}
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
export default function EditStudent({ id }: StudentProps) {
    const [modalShow, setModalShow] = React.useState(false);

    return (
        <>
            <Button className="buttonedit" onClick={() => setModalShow(true)}><MdEdit /></Button>
            <EditStudentForm
                show={modalShow}
                onHide={() => setModalShow(false)}
                id={id}
            />
        </>
    );
}