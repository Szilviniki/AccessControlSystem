"use client";
import React from 'react';
import { Form, Button } from "react-bootstrap"
import Notiflix from "notiflix";
import {login} from "@/actions/loginAction";
import {useCookies} from "next-client-cookies";


function LoginForm() {
    const cookies = useCookies();
    async function onLogin(formData: FormData) {


        if(formData.get("email")==null) {
            Notiflix.Report.warning(
                'Hiba!',
                'Az email cím megdása kötelező megadása kötelező!',
                'Rendben',
            );
        }if (formData.get("password")==null) {
            Notiflix.Report.warning(
                'Hiba!',
                'A jelszó megadása kötelező!',
                'Rendben',
            );
        }
        else {
            const res = await login(formData);
            if (!res.error) {
                cookies.set("email", formData.get("email")as string);

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