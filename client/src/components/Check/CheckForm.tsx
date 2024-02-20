"use client";
import React from 'react';
import {Form, Button, FormGroup} from "react-bootstrap"
import {useCookies} from "next-client-cookies";



function CheckForm() {
    const cookies = useCookies();
    return (
        <form>
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


export default CheckForm ;