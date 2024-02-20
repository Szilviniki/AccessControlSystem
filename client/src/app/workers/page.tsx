import React, {use} from 'react';
import MainTemplate from '@/app/templates/MainTemplate';
import { Card } from 'react-bootstrap';
import Faculities from "@/components/DataTable/Faculities";

export default function WorkersPage() {


    return (
        <MainTemplate>
            <Card className="table">
                <Faculities />
            </Card>
        </MainTemplate>
    );
}
