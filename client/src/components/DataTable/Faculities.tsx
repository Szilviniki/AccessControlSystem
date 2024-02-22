"use client";

import React, {useEffect, useState} from 'react';
import DataTable from "react-data-table-component";

function Faculities() {
    const [faculties, setData] = useState([])

    useEffect(() => {
        fetch(`http://localhost:4001/api/v1/Faculty/GetAll`).then((res) => {
            res.json().then((datas) => {
                setData(datas.data)
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

    function prepareData(datas: any[]) {
        return datas.map((item) => {
            const pres = item.isPresent;
            let status = "jelen2";
            if (pres == true) {
                status = "nincs jelen";
            }
            if (!pres == true) {
                status = "jelen";
            }
            return {
                id: item.id,
                name: item.name,
                role_id: item.role_id,
                present: status
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