"use client";

import React, {use, useEffect, useState} from 'react';
import DataTable, {TableColumn} from "react-data-table-component";
import {ITable} from "@/interfaces/table";

function Faculities() {
    const [faculties, setData] = useState([])

    useEffect(() => {
            fetch(`http://localhost:4001/api/v1/Faculty/GetAll`).then((res) => {
                res.json().then((data) => {
                    setData(data)
                })
            })

    }, [])

    const columns: TableColumn<ITable> = [
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

    function prepareData(data: any[]) {
        return data.map((item) => {
            const pres=item.present;
            let status ="jelen2";
            if (pres==true){
                status="nincs jelen";
            } else {
                status ="jelen";
            }
            return {
                id: item.id,
                name: item.name,
                role_id: item.roleId,
                present:status
            }
        });
    }

    const transformedData = prepareData(faculties);
    console.log(prepareData(faculties))

    return (
        <>
            <DataTable
                columns={columns}
                data={transformedData}
                fixedHeader={true}
                responsive
                expandableRows
            />
        </>
    );
}

export default Faculities;