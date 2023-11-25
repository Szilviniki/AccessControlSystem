"use client";
import {IInput} from "@/interfaces/loginForm";
import {Form as BSForm, Button, Col} from 'react-bootstrap';
import React, {useState} from "react";


export default function Form({
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
            {inputs && inputs.map((input: IInput) => {
                let renderInput = (
                    <BSForm.Control

                        className="inputFc"
                        placeholder={input.label}
                        type={input.type}
                        id={input.id}
                    />
                );
                if (input.type === "button") {
                    renderInput = (
                        <Col sm={12}>
                            <Button
                                id={input.id}
                                type="submit"
                                className="loginBt"
                            >
                                {input.label}
                            </Button>
                        </Col>
                    );
                }

                return (
                    <BSForm.Group
                        className="mb-3"
                        controlId={input.id}
                    >
                        {input.type !== "button"}
                        {renderInput}
                    </BSForm.Group>
                );
            })}
        </form>
    );
}
