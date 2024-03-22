import {Button, Form} from "react-bootstrap";
import React from "react";

function settings (){
    return (
        <form>
            <Form.Group>
                <Form.Control
                    type="name"
                    name="text"
                    placeholder="Név"
                    className="inputFc"
                />
            </Form.Group>
            <Form.Group>
                <Form.Control
                    type="phone"
                    name="text"
                    placeholder="Név"
                    className="inputFc"
                />
            </Form.Group>
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
export default settings;