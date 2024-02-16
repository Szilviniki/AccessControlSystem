'use client'
import Navbar from "react-bootstrap/Navbar";
import {Col, NavbarText, Row} from "react-bootstrap";
import React from "react";
import Moment from 'react-moment';
import 'moment-timezone';
import moment from "moment";
import {timer} from "rxjs";

export default function CheckTopNavigation() {
//let now= times
    { return(
        <Navbar className="fixed-top Top-Nav" >
            <Navbar.Collapse className="justify-content-end ">
                <Row>
                    <Col>
                        <NavbarText className="mx-3 justify-content-start" >

                        </NavbarText>
                    </Col>
                    <Col>
                        <NavbarText className="mx-3 justify-content-end">
                            <Moment interval={1000}  format="hh:mm:ss" durationFromNow />
                        </NavbarText>
                    </Col>
                </Row>

            </Navbar.Collapse>
        </Navbar>
    )
    }
}