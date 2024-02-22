'use client'
import React from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import {FaPlusCircle} from "react-icons/fa";

function AddNewWorkersForm(props:any) {
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
            <Modal.Body >

            </Modal.Body>
            <Modal.Footer>
                <Button className="_new" onClick={props.onHide}>Mentés</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default function AddNewWorkers() {
    const [modalShow, setModalShow] = React.useState(false);

    return (
        <>
            <Button className="_new" onClick={() => setModalShow(true)}>
                <FaPlusCircle className="mx-1 mb-1" />
                Új dolgozó
            </Button>

            <AddNewWorkersForm
                show={modalShow}
                onHide={() => setModalShow(false)}
            />
        </>
    );
}
