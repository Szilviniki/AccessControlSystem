"use client";
import React from 'react';
import { Form, Button } from "react-bootstrap"
import {useCookies} from "next-client-cookies";
import NewPassword from "@/app/newpassword/page";

function NewPasswordForm() {
    const cookies = useCookies();

   /** async function newpassword(formData: FormData) {

        const res = await newpassword(formData);
        console.log("action válasz: ", res)
        if (res==) {
            cookies.set("user", JSON.stringify(res))
            location.href = "/"
        } else {
            let message = res;
            if (Array.isArray(message)) {
                message = message[0]
            }
            Notiflix.Report.warning(
                'Valami nem jó!',
                'Figyeljen oda, hogy minden mező helyesen legyen kitöltve!',
                'Rendben',
            );
        }
    }**/
    return (
        <form action={NewPassword}>
            <Form.Group>
                <Form.Control
                    type="password"
                    name="pasword1"
                    placeholder="Jelszó"
                    className="inputFc"
                />
            </Form.Group>
            <Form.Group>
                <Form.Control
                    type="password"
                    name="password2"
                    placeholder="Jelszó megerősítés"
                    className="inputFc"
                />
            </Form.Group>
            <Form.Group>
                <Button type="submit" className="mt-2 loginBt">Mentés</Button>
            </Form.Group>
        </form>
    );
}


export default NewPasswordForm;