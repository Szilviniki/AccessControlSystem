'use client'
import React, {useEffect, useState} from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import {MdEdit} from "react-icons/md";
import {Col, Container, Form, Row} from "react-bootstrap";
import { useCookies} from "next-client-cookies";
import {StudentProps} from "@/interfaces/Student";
import {updateFaculty} from "@/actions/FacultyUpdate";
import facultyType from "@/types/facultyType";


function EditFacultiesForm(props: any) {
    const [faculty, setData] = useState<facultyType>({name:"",id:"",role:0,isPresent:false,email:"",phone:""});
    const cookies = useCookies();
    const token = (cookies.get("user-token")as string);

    useEffect(() => {
        fetch(`http://localhost:4001/api/v1/Faculty/Get/${props.id}`, {
            headers: {
                "Authorization": "Bearer " + token.replaceAll("\"", "").trim(),
                "Access-Control-Allow-Origin": "*",
            },
            mode: "cors",
        }).then((res) => {
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
                    Dolgozó szerkesztése
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Container className="justify-content-center">
                    <form action={updateFaculty}>
                        <input type="hidden" name={"token"} value={token}/>
                        <input type="hidden" name={"id"} value={props.id}/>
                        <Row className="justify-content-center">
                            <Col className="justify-content-center">
                                <Form.Group>
                                    <Form.Control
                                        defaultValue={faculty.name}
                                        type="text"
                                        name="name"
                                        placeholder="Dolgozó neve"
                                        className="inputFc mb-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        defaultValue={faculty.email}
                                        type="email"
                                        name="email"
                                        placeholder="Dolgozó email cím"
                                        className="inputFc mb-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Control
                                        defaultValue={faculty.phone}
                                        type="text"
                                        name="phone"
                                        placeholder="Dolgozó telefonszám"
                                        className="inputFc mb-4"
                                    />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Select name="day" defaultValue={faculty.role} className="select">
                                        <option value={1}>Admin</option>
                                        <option value={2}>Kollégium vezető</option>
                                        <option value={3}>Nevelő</option>
                                        <option value={4}>Technikai dolgozó</option>
                                    </Form.Select>
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
export default function EditFaculties({ id }: StudentProps){
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