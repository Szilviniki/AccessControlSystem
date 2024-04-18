"use client"
import React from 'react';
import { Form, Button } from "react-bootstrap"
import Notiflix from "notiflix";
import {login} from "@/actions/loginAction";
import {useCookies} from "next-client-cookies";

function LoginForm() {
    const cookies = useCookies();
    async function onLogin(formData: FormData) {

        if((formData.get("email")=="")) {
            Notiflix.Report.failure(
                'Hiba!',
                'Az email cím megdása kötelező megadása kötelező!',
                'Rendben',
            );
        }if (formData.get("password")=="") {
            Notiflix.Report.failure(
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

                Notiflix.Report.failure(
                    'Valami nem jó!',
                    'Figyeljen oda, hogy minden mező helyesen legyen kitöltve!',
                    'Rendben',
                );
            } else {

                cookies.set("user-name", JSON.stringify(res.name));
                cookies.set("user-email", JSON.stringify(res.email));
                cookies.set("user-role", JSON.stringify(res.role));
                cookies.set("user-id", JSON.stringify(res.id));
                cookies.set("user-token", JSON.stringify(res.token));
                location.href = "/"
            }
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
                <Button type="submit" className="mt-2 loginBt">Bejelentkezés</Button>
            </Form.Group>
        </form>
    );
}


export default LoginForm;