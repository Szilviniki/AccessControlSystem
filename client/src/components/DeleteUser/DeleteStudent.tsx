import React from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import {MdDelete} from "react-icons/md";
import { StudentProps} from "@/interfaces/Student";
import {useCookies} from "next-client-cookies";

function DeleteStudentForm(props: any) {
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
            <Modal.Header>
                <Modal.Title id="contained-modal-title-vcenter">
                   Diák törlése
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                Biztos hogy törli a diákot?
            </Modal.Body>
            <Modal.Footer>
                <Button className="buttondelete" onClick={props.onHide} >Mégsem</Button>
                <Button className="buttonedit" onClick={() => {
                    fetch(`http://localhost:4001/api/v1/Students/Delete/${props.id}`, {
                        method: 'DELETE',
                        headers: {
                            "Authorization": "Bearer " + token.replaceAll("\"", "").trim(),
                            "Access-Control-Allow-Origin": "*",
                        },
                        mode: "cors",
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


export default function DeleteStudent({ id }: StudentProps) {
    const [modalShow, setModalShow] = React.useState(false);

    return (
        <>
            <Button className="buttondelete" onClick={() => setModalShow(true)}><MdDelete /></Button>
            <DeleteStudentForm
                show={modalShow}
                onHide={() => setModalShow(false)}
                id={id}
            />
        </>
    );
}
