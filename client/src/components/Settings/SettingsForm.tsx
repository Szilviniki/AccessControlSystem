"use client"
import {Button, Col, Row, Form} from "react-bootstrap";
import React, {useEffect, useState} from "react";
import {useCookies} from "next-client-cookies";
import Notiflix from "notiflix";
import {login} from "@/actions/loginAction";


function settings() {
    const cookies = useCookies();
    const [data, setData] = useState([])

    useEffect(() => {
        fetch(`http://localhost:4001/api/Guardian/{${cookies.get("user-email")}}`).then((res) => {
            res.json().then((datas) => {
                setData(datas.data)
            })
        })


    }, [])
    async function update(formData: FormData ) {

        if((formData.get("email")=="")) {
            Notiflix.Report.warning(
                'Hiba!',
                'Az email cím megdása kötelező megadása kötelező!',
                'Rendben',
            );
        }if (formData.get("password")=="") {
            Notiflix.Report.warning(
                'Hiba!',
                'A jelszó megadása kötelező!',
                'Rendben',
            );
        }
        else {
            const res = await login(formData);
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
    }
    return (
        <Row>
            <Col sm={12} md={12}>
                <form action={update}>
                    <Row className="justify-content-center">
                        <Col sm={12} md={11}>
                            <Form.Group>
                                <Form.Label>
                                    Név
                                </Form.Label>
                                <Form.Control
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
                                    type="email"
                                    name="email"
                                    placeholder="Email cím"
                                    className="inputFc"
                                />
                            </Form.Group>
                        </Col>
                        <Col md={6} >
                            <Form.Group>
                                <Button type="submit" className="mt-2 loginBt">Mentés</Button>
                            </Form.Group>
                        </Col>
                    </Row>
                </form>
            </Col>
        </Row>
    );
}

export default settings;