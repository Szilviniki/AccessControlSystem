'use client'

import React, {use, useState} from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import {FaUserTag} from "react-icons/fa";
import {Container, Form} from "react-bootstrap";
import {save} from "@/actions/NewStudentAction";

async function Get()  {
    return await (await fetch(`http://localhost:4001/api/v1/Student/GetAll`)).json()
}

function AddNewStudentForm(props: any) {
    const data = use(Get()).data;
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
                    Új feljegyzés
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Container className="justify-content-center">
                    <form action={save}>
                        <Form.Group>
                            <Form.Select name="day" aria-label="Default select example">
                                <option>Diák neve</option>
                                {data && data.map(student => (

                                        <option value={student.id} >{student.name}</option> ))}
                            </Form.Select>
                        </Form.Group>
                        <Form.Group>
                            <Form.Control
                                type="text"
                                name="name"
                                placeholder="Feljegyzés tárgya"
                                className="inputFc m-4"
                            />
                        </Form.Group>
                        <Form.Group>
                            <Form.Select name="day" aria-label="Default select example">
                                <option>Ezen a napon</option>
                                <option value="1" >Hétfő</option>
                                <option value="2">Kedd</option>
                                <option value="3">Szerda</option>
                                <option value="4">Csütörtök</option>
                                <option value="5">Péntek</option>
                            </Form.Select>
                        </Form.Group>
                        <Form.Group>
                            <Button type="submit" className="mt-2 loginBt"
                                    onClick={props.onHide}>Mentés</Button>
                        </Form.Group>
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
                <FaUserTag  className="mx-1 mb-1"/>
                Új feljegyzés
            </Button>

            <AddNewStudentForm
                show={modalShow}
                onHide={() => setModalShow(false)}
                {...props}
            />
        </>
    );
}
