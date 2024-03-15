'use client'
import Navbar from "react-bootstrap/Navbar";
import {Col, NavbarText, Row} from "react-bootstrap";
import React, {useEffect, useState} from "react";
import Moment from 'react-moment';
import 'moment-timezone';
import moment from "moment";
import {timer} from "rxjs";

export default function CheckTopNavigation() {
//let now= times
    const [time, setTime] = useState(moment());

    useEffect(() => {
        setInterval(() => {
            setTime(moment())
        }, 1000)
    }, []);
    { return(
        <Navbar className="fixed-top Top-Nav" style={{
            'borderRadius': "50px 50px 0 0"
        }}>
            <Navbar.Collapse className="justify-content-between ">
                    <div>
                        <NavbarText className="mx-3 justify-content-start" >
                            {time.format("Y.MM.DD")}
                        </NavbarText>
                    </div>
                    <div>
                        <NavbarText className="mx-3 justify-content-end">
                            {time.format("HH:mm:ss")}
                        </NavbarText>
                    </div>
            </Navbar.Collapse>
        </Navbar>
    )
    }
}