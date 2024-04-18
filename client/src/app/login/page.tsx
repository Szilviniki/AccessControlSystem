import {Col, Row} from "react-bootstrap";
import LoginForm from "@/components/Login/LoginForm";
import Template from "@/app/templates/Template";
import {FaUserCircle} from "react-icons/fa";

export default function Login() {
    return (
        <Template>
            <Row className=" justify-content-center ">
                <Col sm={6} md={12} lg={7}>
                    <FaUserCircle  className="m-10 h-auto" size="50%" color="#83C5BE"/>
                </Col>

            </Row>
            <Row className=" justify-content-center ">
                <Col sm={5} md={7}>
                    <LoginForm />
                </Col>
            </Row>
        </Template>
);
}
