import MainTemplate from "@/app/templates/MainTemplate";
import Students from "@/components/DataTable/Students";
import {Card} from "react-bootstrap";

export default function StudentPage(){
    return (
        <MainTemplate>
            <Card className="table">
               <Students/>
            </Card>
        </MainTemplate>
    );
}

