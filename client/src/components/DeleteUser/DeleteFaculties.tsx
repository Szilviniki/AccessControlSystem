'use client'
import React from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import {MdDelete} from "react-icons/md";
import {DeleteFacultiesProps} from "@/interfaces/Faculties";

function DeleteFacultiesForm(props: any) {
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
                    Dolgozó törlése
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                Biztos hogy törli a dolgozót?
            </Modal.Body>
            <Modal.Footer>
                <Button className="buttondelete" onClick={props.onHide} >Mégsem</Button>
                <Button className="buttonedit" onClick={() => {
                    fetch(`http://localhost:4001/api/v1/Faculty/Delete/${props.id}`, {
                        method: 'DELETE'


                    }).then(data => {
                        location.reload()
                    })

                    props.onHide
                }}>
                    Igen
                </Button>
            </Modal.Footer>
        </Modal>
    );
}


export default function DeleteFaculties({ id }: DeleteFacultiesProps) {
    const [modalShow, setModalShow] = React.useState(false);

    return (
        <>
            <Button className="buttondelete" onClick={() => setModalShow(true)}><MdDelete /></Button>
            <DeleteFacultiesForm
                show={modalShow}
                onHide={() => setModalShow(false)}
                id={id}
            />
        </>
    );
}