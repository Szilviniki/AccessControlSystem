import Template from "@/app/templates/Template";
import CheckTopNavigation from "@/components/Navbar/CheckTopNavigation";
import {Col, Row} from "react-bootstrap";
import CheckForm from "@/components/Check/CheckForm";

export default function CheckPage(){
    return(
        <Template>
      <CheckTopNavigation/>
            <Row className=" justify-content-center ">
                <Col sm={6} md={6} lg={6} className="mb-4">
                   <h1> Tartsd a
                    diákigazolványodat
                    a leolvasó alá!</h1>
                </Col>
            </Row>
            <Row className=" justify-content-center ">
                <Col sm={5} md={7}>
                    <CheckForm/>
                </Col>
            </Row>
        </Template>
    )
}