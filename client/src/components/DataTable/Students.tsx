"use client";

import React, {useEffect, useState} from 'react';
import DataTable, {Direction} from "react-data-table-component";
import moment from "moment";
import {FaUserCheck, FaUserTimes} from "react-icons/fa";
import {Row, Col, Container, ButtonGroup} from "react-bootstrap";
import DeleteStudent from "@/components/DeleteUser/DeleteStudent";
import EditStudent from "@/components/EditUser/EditStudent";
import AddNotes from "@/components/NewNote/NewNote";
import {useCookies} from "next-client-cookies";
import studentType from "@/types/studentType";
import noteType from "@/types/noteType";


function Students() {
    const [students, setStudents] = useState<studentType[]|never[]>([])
    const cookies = useCookies();
    const token = (cookies.get("user-token") as string);
    const role = (cookies.get("user-role") || "") as string;


    useEffect(() => {
        fetch(`http://localhost:4001/api/v1/Students/GetAll`,{
            headers: {
                "Authorization": "Bearer " + token.replaceAll("\"", "").trim(),
                "Access-Control-Allow-Origin": "*",
            },
            mode: "cors",
        }).then((res) => {

            res.json().then((datas) => {
                setStudents(datas.data)


            })
        })

    }, [])

    function prepareData(datas: studentType[]) {

        return datas ? datas.map((item) => {
            return {

                id: item.id,
                name: item.name,
                email:(<a href={`mailto:${item.email}`}>{item.email}</a>),
                age: moment().diff(moment(item.birthDate), "years"),
                birthDate: moment(item.birthDate).format("YYYY.MM.DD"),
                phone: (<a href={`tel:${item.phone}`}>{item.phone}</a>),
                present: item.isPresent ? (<FaUserCheck size="8%" color="#006D77" />) : (<FaUserTimes size="8%" color="E29578"/>),
                pname: item.parent.name,
                pemail: (<a href={`mailto:${item.parent.email}`}>{item.parent.email}</a>),
                pphone:(<a href={`tel:${item.parent.phone}`}>{item.parent.phone}</a>),
                notes:item.notes

            }
        }) : [];
    }

    const transformedData = prepareData(students);

    const ExpandedComponent = ({ data }:any) => <pre>{

        <Container>
            <Row>
                <Col className="expanded">
                        <h6>Diák adatai</h6>
                        Születési dátum: {data.birthDate}
                         <br/>
                        Email cím: {data.email}
                        <br/>
                        Telefonszám: {data.phone}
                </Col>
                <Col className="expanded">

                    <h6>Szülő adatai</h6>
                    Név: {data.pname}
                    <br/>
                    Email cím: {data.pemail}
                    <br/>
                    Telefonszám: {data.pphone}
                </Col>
                <Col className="expanded">
                    <ButtonGroup aria-label="Basic example">
                        <AddNotes id={data.id}/>
                        {(role=="\"1\"" || role=="\"2\"") && (
                            <>
                                <EditStudent id={data.id}/>
                                <DeleteStudent id={data.id}/>
                            </>
                        )}
                    </ButtonGroup>
                    </Col>
                </Row>
            <Row>
                <Col className="expanded">
                   <h6>Feljegyzések:</h6>
                    {data && data.notes.map((note:noteType) => (
                        <p key={note.id}>-{note.name}</p>
                    ))}
                </Col>
            </Row>
            </Container>

    }</pre>;

    const columns: {
        name: string;
        selector: (row: any) => any;
        sortable: boolean;
    }[] = [
        {
            name: 'Név',
            selector: (row: any) => row.name,
            sortable: true
        },
        {
            name: 'Életkor',
            selector: (row: any) => row.age,
            sortable: true
        },
        {
            name: 'Státusz',
            selector: (row: any) => row.present,
            sortable: false

        }
    ];

    return (
        <>
            <DataTable
                columns={columns}
                data={transformedData}
                fixedHeader={true}
                direction={Direction.LTR}
                responsive
                striped
                expandableRows
                expandableRowsComponent={ExpandedComponent}
            />
        </>
    );
}

export default Students;


