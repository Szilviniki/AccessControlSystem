"use client";
import React from 'react';
import {Form, Button, FormGroup} from "react-bootstrap"
import Notiflix, {Notify} from "notiflix";
import {check} from "@/actions/checkAction";

export default function CheckForm() {
    async function onCheck(formData: FormData) {

        const res = await check(formData);
        if (!res.error) {
            Notify.success('Sikeres Belépés/Kilépés');

        } else {
            let message = res.messages;
            if (Array.isArray(message)) {
                message = message[0]
            }
            Notify.failure('Sikertelen Belépés/Kilépés! ', {
                timeout: 6000,
            } );
        }
    }

    return (
        <form action={onCheck}>
            <Form.Group>
                <Form.Control
                    type="text"
                    name="code"
                    placeholder="Belépőkód (diákigazolvány szám)"
                    className="inputFc"
                />
            </Form.Group>
            <Form.Group>
                <Button type="submit" className="mt-2 loginBt">Belépés/Kilépés</Button>
            </Form.Group>
        </form>
    );
}
