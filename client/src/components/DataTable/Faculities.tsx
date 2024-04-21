"use client";

import React, {useEffect, useState} from 'react';
import DataTable from "react-data-table-component";
import {FaUserCheck, FaUserTimes} from "react-icons/fa";
import {ButtonGroup, Col, Container, Row} from "react-bootstrap";
import EditFaculties from "@/components/EditUser/EditFaculties";
import DeleteFaculties from "@/components/DeleteUser/DeleteFaculties";
import {useCookies} from "next-client-cookies";

function Faculities() {
    const [faculties, setData] = useState([])
    const transformedData = prepareData(faculties)
    const cookies = useCookies();
    const token = (cookies.get("user-token") as string);
    const role = (cookies.get("user-role" ||"") as string);

    useEffect(() => {
        fetch(`http://localhost:4001/api/v1/Faculty/GetAll`,{
            headers:{
                "Authorization": "Bearer " + token.replaceAll("\"", "").trim(),
                "Access-Control-Allow-Origin": "*",
            },
            mode: "cors",
        }).then((res) => {
            res.json().then((datas) => {
                setData(datas.data)

            })
        })

    }, [])

    function prepareData(datas: any[]) {
        return datas.map((item) => {

            if(item.role==1){
                item.role = "Admin"
            }
            else if(item.role==2){
                item.role = "Kollégium vezető"
            }
            else if(item.role==3){
                item.role = "Nevelő"
            }
            else if(item.role==4){
                 item.role = "Technikai dolgozó"
            }
            return {
                id: item.id,
                name: item.name,
                email:(<a href={`mailto:${item.email}`}>{item.email}</a>),
                role_id: item.role,
                phone:(<a className="_Link" href={`tel:${item.phone}`}>{item.phone}</a>),
                present: item.isPresent ?(<FaUserCheck size="8%" color="#006D77" />) : (<FaUserTimes size="8%" color="E29578"/>),
            }
        });
    }


    const ExpandedComponent = ({ data }:any) => <pre>{
        <Container>
        <Row>
        <Col className="expanded">
        <br/>
                        Email cím: {data.email}
        <br/>
                        Telefonszám: {data.phone}
                </Col>
    <Col className="expanded">
        <ButtonGroup aria-label="Basic example">
            {(role=="\"1\"") && (
                <>
                    <EditFaculties id={data.id}/>
                    <DeleteFaculties id={data.id}/>
                </>
            )}
        </ButtonGroup>
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
            name: 'Beosztás',
            selector: (row: any) => row.role_id,
            sortable: false
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
                responsive
                striped
                expandableRows
                expandableRowsComponent={ExpandedComponent}
            />
        </>
    );
}

export default Faculities;