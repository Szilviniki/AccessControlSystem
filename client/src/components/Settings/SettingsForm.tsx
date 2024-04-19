"use client"

import {Button, Col, Row, Form} from "react-bootstrap";
import React, {useEffect, useState} from "react";
import {useCookies} from "next-client-cookies";
import Notiflix from "notiflix";
import {updateUser} from "@/actions/userDataUpdateAction";


export default function settings() {
    const cookies = useCookies();
    const id = ((cookies.get("user-id")as string).replaceAll('"', ''));
    const token = (cookies.get("user-token") as string);
    const [user, setData] = useState([])

    useEffect(() => {
        fetch(`http://localhost:4001/api/v1/Faculty/Get/${id}`,{
            headers: {
            "Authorization": "Bearer " + token.replaceAll("\"", "").trim(),
                "Access-Control-Allow-Origin": "*",
        },
        mode: "cors",}
        ).then((res) => {
            res.json().then((datas) => {
                setData(datas.data)
            })
        })
    }, [])

    async function update(formData: FormData ) {

            const res = await updateUser(formData);
            if (res.queryIsSuccess==false) {
                let message = res.message;
                if (Array.isArray(message)) {
                       message = message[0]
                }

                Notiflix.Report.warning(
                    'Sikertelen módósítás!',
                    'Figyeljen oda, hogy minden mező helyesen legyen kitöltve!',
                    'Rendben',
                );
            } else {
                Notiflix.Report.success(
                    'Sikeres módosítás!',
                    'Sikeresen módósította az adatait!',
                    'Rendben',
                );

            }
        }

    return (
        <Row>
            <Col sm={12} md={12}>
                <form action={update}>
                    <Row className="justify-content-center">
                        <Col sm={12} md={11}>
                            <Form.Group>
                                <input type="hidden" name={"token"} value={token}/>
                                <input type="hidden" name={"id"} value={id}/>
                                <Form.Label>
                                    Név
                                </Form.Label>
                                <Form.Control
                                    defaultValue={user.name}
                                    type="name"
                                    name="text"
                                    placeholder="Név"
                                    className="inputFc"

                                />
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>
                                    Telefonszám
                                </Form.Label>
                                <Form.Control
                                    defaultValue={user.phone}
                                    type="phone"
                                    name="text"
                                    placeholder="Telefonszám"
                                    className="inputFc"
                                />
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>
                                    Email cím
                                </Form.Label>
                                <Form.Control
                                    defaultValue={user.email}
                                    type="email"
                                    name="email"
                                    placeholder="Email cím"
                                    className="inputFc"
                                />
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>
                                    Új jelszó
                                </Form.Label>
                                <Form.Control
                                    type="password"
                                    name="newpassword"
                                    placeholder="Új jelszó"
                                    className="inputFc"
                                />
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>
                                    Új jelszó mégegyszer
                                </Form.Label>
                                <Form.Control
                                    type="password"
                                    name="pass"
                                    placeholder="Új jelszó mégegyszer"
                                    className="inputFc"
                                />
                            </Form.Group>
                        </Col>
                        <Col md={6} >
                            <Form.Group>
                                <Button type="submit" className="mt-1 loginBt">Mentés</Button>
                            </Form.Group>
                        </Col>
                    </Row>
                </form>
            </Col>
        </Row>
    );
}

