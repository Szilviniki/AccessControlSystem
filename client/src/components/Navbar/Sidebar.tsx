"use client";

import {
    CDBSidebar,
    CDBSidebarHeader,
    CDBSidebarContent,
    CDBSidebarMenu,
    CDBSidebarMenuItem, CDBSidebarFooter
} from "cdbreact";
import Link from "next/link";
import { FaBars } from "react-icons/fa";
import {Col, Row} from "react-bootstrap";
import {BsHouseLockFill} from "react-icons/bs";
import {useCookies} from "next-client-cookies";


export default function SideMenu() {
    const cookies=useCookies();

    return (
        <div
            className="d-flex"
            style={{height: '100vh', overflow: 'scroll initial' }}
        >
                <CDBSidebar className="" textColor="#EDF6FF" backgroundColor="#006D77" breakpoint={720} toggled={false} minWidth="60%" maxWidth="90%">
                <CDBSidebarHeader prefix={<FaBars />} >
                    <BsHouseLockFill size={40} style={{marginRight:15}}/>
                    ACS
                </CDBSidebarHeader>
                <CDBSidebarContent>
                    <CDBSidebarMenu>
                        
                        <Link href="/" className="Item">
                            <CDBSidebarMenuItem icon="home" iconSize="2x" >
                                <p className="m-4" > Kezdőlap</p>
                            </CDBSidebarMenuItem>
                        </Link>
                    </CDBSidebarMenu>
                    <CDBSidebarMenu>
                        <Link href="/students" className="Item">
                            <CDBSidebarMenuItem icon="user-friends" iconSize="2x" >
                                <p className="m-4"> Diákok</p>
                            </CDBSidebarMenuItem>
                        </Link>
                    </CDBSidebarMenu>
                    <CDBSidebarMenu>
                        <Link href="/workers" className="Item">
                            <CDBSidebarMenuItem icon="user-tie" iconSize="2x" >
                              <p className="m-4"> Dolgozók</p>
                            </CDBSidebarMenuItem>
                        </Link>
                    </CDBSidebarMenu>
                    <CDBSidebarMenu>
                        <Link href="/notes" className="Item">
                            <CDBSidebarMenuItem icon="user-edit" iconSize="2x" >
                                <p className="m-4"> Feljegyzések</p>
                            </CDBSidebarMenuItem>
                        </Link>
                    </CDBSidebarMenu>
                    {/*<CDBSidebarMenu>
                        <Link href="/mail" className="Item" >
                            <CDBSidebarMenuItem icon="envelope"  iconSize="2x" >
                                <p className="m-4"> Üzenetek</p>
                            </CDBSidebarMenuItem>
                        </Link>
                    </CDBSidebarMenu>*/}

                </CDBSidebarContent>
                <CDBSidebarFooter>
                    <Row>
                        <Col lassName="justify-content-between mb-4">
                            <Link href="/settings" className="Item">
                                <CDBSidebarMenuItem icon="cog" iconSize="2x"></CDBSidebarMenuItem>
                            </Link>
                        </Col>
                        <Col className="justify-content-between mb-4">
                            <Link href="/login" className="Item" onClick={()=>{
                                cookies.remove("user")}}>
                                <CDBSidebarMenuItem icon="sign-out-alt" iconSize="2x" className="d-flex justify-content-between"  ></CDBSidebarMenuItem>
                            </Link>
                        </Col>
                    </Row>
                </CDBSidebarFooter>
            </CDBSidebar>
        </div>
    )
}