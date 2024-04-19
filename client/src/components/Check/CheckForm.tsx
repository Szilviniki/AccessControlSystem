"use client";
import React, {useRef} from 'react';
import {Form, Button} from "react-bootstrap"
import {Notify} from "notiflix";
import {checkWorkers} from "@/actions/checkWorkersAction";
import {checkStudent} from "@/actions/checkStudentAction";

export default function CheckForm() {
    const inputRef = useRef({} as HTMLInputElement);
    async function onCheck(formData: FormData) {

        const resWorkers = await checkWorkers(formData);
        const resStudent= await checkStudent(formData);

        if (resWorkers.queryIsSuccess===true || resStudent.queryIsSuccess===true) {
            Notify.success('Sikeres Belépés/Kilépés');

            inputRef.current.value = ""
        } else {
            Notify.failure('Sikertelen Belépés/Kilépés! ', {
                timeout: 6000,
            });
        }
    }

    return (
        <form action={onCheck}>
            <Form.Group>
                <Form.Control
                    type="number"
                    name="code"
                    placeholder="Belépőkód (diákigazolvány szám)"
                    className="inputFc"
                    maxLength={10}
                    ref={inputRef}
                />
            </Form.Group>
            <Form.Group>
                <Button type="submit" className="mt-2 loginBt">Belépés/Kilépés</Button>
            </Form.Group>
        </form>
    );
}
