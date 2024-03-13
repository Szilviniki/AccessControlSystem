"use client";

import React, {useEffect, useState} from 'react';
import DataTable, {Direction} from "react-data-table-component";
import moment from "moment";
import {FaCheck, FaTimes} from "react-icons/fa";

function Students() {
    const [students, setStudents] = useState([])
    const [activedate, setactivedate] = useState(moment().locale("hu"));


    useEffect(() => {
        fetch(`http://localhost:4001/api/v1/Students/GetAll`).then((res) => {
            res.json().then((datas) => {
                setStudents(datas.data)
                console.log(datas)
            })
        })

    }, [])

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
            name: 'Telefonszám',
            selector: (row: any) => row.phone,
            sortable: true
        },
        {
            name: 'Életkor',
            selector: (row: any) => row.birthDate,
            sortable: true
        },
        {
            name: 'Gondviselő',
            selector: (row: any) => row.phone,
            sortable: true
        },
        {
            name: 'Gondviselő telefonszáma',
            selector: (row: any) => row.phone,
            sortable: true
        },
        {
            name: 'Státusz',
            selector: (row: any) => row.present,
            sortable: false
        }
    ];


    function prepareData(datas: any[]) {

        return datas.map((item) => {
            return {
                id: item.id,
                name: item.name,
                birthDate: moment().diff(moment(item.birthDate), "years"),
                phone: (<a href={`tel:${item.phone}`}>{item.phone}</a>),
                present: item.isPresent ? (<FaCheck />) : (<FaTimes />),
            }
        });
    }

    const transformedData = prepareData(students);
    console.log(prepareData(students))

    return (
        <>
            <DataTable
                columns={columns}
                data={transformedData}
                fixedHeader={true}
                direction={Direction.LTR}
                responsive
                striped
            />
        </>
    );
}

export default Students;