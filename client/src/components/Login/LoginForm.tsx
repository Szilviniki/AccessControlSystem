"use client";
import React from 'react';
import { Form, Button } from "react-bootstrap"
import Notiflix from "notiflix";
import {useCookies} from "next-client-cookies";
import {login} from "@/actions/loginForm";

function LoginForm() {
    const cookies = useCookies();

    async function onLogin(formData: FormData) {

        const res = await login(formData);
        console.log("action válasz: ", res)
        if (!res.error) {
            cookies.set("user", JSON.stringify(res))
            location.href = "/"
        } else {
            let message = res.messages;
            if (Array.isArray(message)) {
                message = message[0]
            }
            Notiflix.Report.warning(
                'Valami nem jó!',
                'Figyeljen oda, hogy minden mező helyesen legyen kitöltve!',
                'Rendben',
            );
        }
    }
    return (
        <form action={onLogin}>
            <Form.Group>
                <Form.Control
                    type="email"
                    name="email"
                    placeholder="Email cím"
                    className="inputFc"
                />
            </Form.Group>
            <Form.Group>
                <Form.Control
                    type="password"
                    name="password"
                    placeholder="Jelszó"
                    className="inputFc"
                />
            </Form.Group>
            <Form.Group>
                <Button type="submit" variant="primary" className="mt-2 loginBt">Bejelentkezés</Button>
            </Form.Group>
        </form>
    );
}


export default LoginForm;