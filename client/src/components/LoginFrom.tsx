"use client";
import { IInput } from "@/interfaces/loginForm";
import { Form as BSForm, Button, Col } from 'react-bootstrap';
import React, { useState } from "react";

export default function CustomForm({
                                       inputs,
                                       onSubmitFunction
                                   }: {
    inputs: Array<IInput>,
    onSubmitFunction: Function
}) {
    const [formInputValues, setFormInputValues] = useState({});

    return (
        <form onSubmit={(e) => {
            e.preventDefault();
            onSubmitFunction(formInputValues);
        }}>
            {inputs && inputs.map((input: IInput, index) => {
                return (
                    <BSForm.Group
                        key={index}
                        className="mb-3"
                    >
                        {input.type !== "button" ? (
                            <BSForm.Control
                                className="inputFc"
                                placeholder={input.label}
                                type={input.type}
                                onChange={(e) => {
                                    setFormInputValues((prevValues) => ({
                                        ...prevValues,
                                        [input.id]: e.target.value,
                                    }));
                                }}
                            />
                        ) : (
                            <Col sm={12}>
                                <Button
                                    type="submit"
                                    className="loginBt"
                                >
                                    {input.label}
                                </Button>
                            </Col>
                        )}
                    </BSForm.Group>
                );
            })}
        </form>
    );
}

export interface FormValues {
    username: string;
    password: string;
}
