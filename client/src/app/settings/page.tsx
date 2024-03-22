import {Button, Form} from "react-bootstrap";
import React from "react";
import MainTemplate from "@/app/templates/MainTemplate";
import {cookies} from "next/headers";
import {redirect} from "next/navigation";

export default function SettingsPage() {
    const user = cookies().get("user");

    if (!user) {
        redirect("/user")
    }
    else {

        return(

            <>

                <MainTemplate>
                    <form>
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
                </MainTemplate>
            </>
        )
    }
}
