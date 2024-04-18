'use client'

import React, {useEffect, useState} from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import {MdEdit} from "react-icons/md";
import {Container, Form} from "react-bootstrap";
import {useCookies} from "next-client-cookies";
import {INotesProps} from "@/interfaces/Notes";
import {SaveNotes} from "@/actions/newNote";
import {FaNoteSticky} from "react-icons/fa6";

function AddNotesForm(props: any) {
    const cookies = useCookies();
    const token = (cookies.get("user-token") as string);

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
                    <form action={SaveNotes}>
                        <input type="hidden" name={"token"} value={token}/>
                        <input type="hidden" name={"id"} value={props.id}/>
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
                                <option value="1">Hétfő</option>
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

export default function AddNotes({id}: INotesProps) {
    const [modalShow, setModalShow] = React.useState(false);
    const cookies = useCookies();
    const token = (cookies.get("user-token") as string);
    return (
        <>
            <Button className="newnotes" onClick={() => setModalShow(true)}><FaNoteSticky /></Button>
            <AddNotesForm
                show={modalShow}
                onHide={() => setModalShow(false)}
                id={id}
                token={token}
            />
        </>
    );
}