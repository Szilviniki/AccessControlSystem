"use client";

import React, {use, useEffect, useState} from 'react';
import DataTable, {TableColumn} from "react-data-table-component";

function Students() {
    const [students, seStudents] = useState([])

    useEffect(() => {
        fetch(`http://localhost:4001/api/v1/Students/GetAll`).then((res) => {
            res.json().then((datas) => {
                seStudents(datas.data)
                console.log(datas)
            })
        })

    }, [])

    const columns: TableColumn<any> = [
        {
            name: 'Név',
            selector: (row: any) => row.name,
            sortable: true
        },
        {
            name: 'Osztály',
            selector: (row: any) => row.class,
            sortable: false
        },
        {
            name: 'Csoport',
            selector: (row: any) => row.groupId,
            sortable: false
        },
        {
            name: 'Státusz',
            selector: (row: any) => row.present,
            sortable: false
        }
    ];

    function prepareData(datas: any[]) {

        return datas.map((item) => {
            const pres=item.isPresent;
            let status:string ="jelen";
            if (pres==false){
               status="nincs jelen";
            } else {
                status ="jelen";
            }
            return {
                id: item.id,
                name: item.name,
                class:item.class,
                groupId: item.groupId,
                present:status
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
                responsive
                expandableRows
            />
        </>
    );
}

export default Students;