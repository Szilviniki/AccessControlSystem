'use client'

import {use, useEffect, useState} from "react";
import {Card, Col, Container, Row} from "react-bootstrap";
import {PiListFill} from "react-icons/pi";
import {FaStar, FaUserEdit, FaUserTimes} from "react-icons/fa";
import moment from "moment";
import {FaUserCheck} from "react-icons/fa6";
import {useCookies} from "next-client-cookies";

async function Get(token: string)  {

    const response = await fetch(`http://localhost:4001/api/v1/Homepage`, {
        headers: {
            "Authorization": "Bearer " + token.replaceAll("\"", "").trim(),
            "Access-Control-Allow-Origin": "*",
        },
        mode: "cors",
    })
    const data = await response.json()

    return data
}

function HomePageForm() {
 const [data, setData] = useState(null);
const cookies = useCookies();

useEffect(() => {
     Get(cookies.get("user-token") as string).then((r) => {
         setData(r.data)
     })
 }, [])
 return data && (
         <Container>
             <Row>
                 <Col>
                     <Card className="CardHP justify-content-center align-items-center d-flex" id="present" >
                         <FaUserCheck  size="3rem" className=""/>
                         <h3>Jelen lévő diákok</h3>
                         <h2>{data.presentStudents}</h2>
                     </Card>
                 </Col>
                 <Col>
                     <Card className="CardHP justify-content-center align-items-center d-flex" id="not-present">
                         <FaUserTimes  size="3rem" className=""/>
                         <h3>Távolévő diákok</h3>
                         <h2>{data.absentStudents}</h2>
                     </Card>
                 </Col>
                 <Col>
                     <Card className="CardHP justify-content-center align-items-center d-flex" id="problematic">
                         <FaUserEdit  size="3rem" className=""/>
                         <h3>Feljegyzések száma</h3>
                         <h2>{data.naughtyStudents}</h2>
                     </Card>
                 </Col>
             </Row>

             <Row>
                 <Col>
                     <Col>
                         <Card className="CardHP">
                             <Row>
                                 <Col md={2}>
                                     <PiListFill size="2rem" className="m-1"/>
                                 </Col>
                                 <Col md={10}>
                                     <h2>Legutóbbi ki és belépések</h2>
                                     <div style={{textAlign: "left"}}>
                                         {data && data.lastLogs.map(log => (
                                             <p key={log.personId} style={{textAlign: "left"}}>
                                                 <b>{moment(log.stamp).format('YYYY.MM.DD - h:mm:ss')}</b> -

                                                 {log.personId}
                                             </p>
                                         ))}
                                     </div>
                                 </Col>
                             </Row>
                         </Card>
                     </Col>
                 </Col>
                 <Col>
                     <Col>
                         <Card className="CardHP">
                             <Row>
                                 <Col md={2}>
                                     <FaStar size="2rem" className="m-1"/>
                                 </Col>
                                 <Col md={10}>
                                     <h2>Feljegyzések</h2>
                                 </Col>
                                 <div>
                                     {data.notices}
                                 </div>
                             </Row>
                         </Card>
                     </Col>

                 </Col>
             </Row>
         </Container>
     );
}

export default HomePageForm;