'use client'
import MainTemplate from "@/app/MainTemplate";
import { CDBCard, CDBCardBody, CDBContainer, CDBDataTable } from "cdbreact";
import {IStudent} from "@/interfaces/student";
import {
    useEffect,
    useState
} from "react";

function testClickEvent(param:any) {
    alert('Row Click Event');
}

const Datas = () => {
    const [data, setData] = useState([]);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        try {
            const response = await fetch('http://localhost:4001/api/v1/Students/Get');
            if (!response.ok) {
                throw new Error('Hiba történt az adatok lekérése közben.');
            }
            const jsonData = await response.json();
            setData(jsonData);
        } catch (error) {
            console.error('Hiba:', error);
        }
    };

    const tableData = {
        columns: [
            {
                label: 'Név',
                field: 'name',
                sort: 'asc',
                width: 150,
                attributes: {
                    'aria-controls': 'DataTable',
                },
            },
            {
                label: 'Osztály',
                field: 'group',
                width: 270,
            },
            {
                label: 'Csoport',
                field: 'class',
                width: 200,
            },
            {
                label: 'Státusz',
                field: 'status',
                width: 100,
            },
        ],
        rows: data.map((student:IStudent) => ({
            name: student.name,
            group: student.groupId,
            class:"hiányzik",
            status: student.isPresent
        }))
    };

    return (
        <MainTemplate>
            <CDBContainer>
                <CDBCard>
                    <CDBCardBody>
                        <CDBDataTable
                            sortable={true}
                            striped={false}
                            bordered
                            hover
                            entriesOptions={[10, 20, 50, 100]}
                            entries={10}
                            entriesLabel="Diákok száma az oldalon"
                            pagesAmount={4}
                            data={tableData}
                            materialSearch={true}
                            searchLabel="Keresés"
                            bordered-color="red"
                            noRecordsFoundLabel="Nem található diák"
                            paging={true}
                            paginationLabel={["<", ">"]}
                            info={false}
                        />
                    </CDBCardBody>
                </CDBCard>
            </CDBContainer>
        </MainTemplate>
    );
};

export default Datas;
