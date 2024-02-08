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


export default function SideMenu() {
    return (
        <div
            className="d-flex"
            style={{height: '100vh', overflow: 'scroll initial' }}
        >
            <CDBSidebar textColor="#EDF6FF" backgroundColor="#006D77" className="">
                <CDBSidebarHeader prefix={<FaBars />} >
                    <BsHouseLockFill size={40} />
                </CDBSidebarHeader>
                <CDBSidebarContent>
                    <CDBSidebarMenu>
                        
                        <Link href="/">
                            <CDBSidebarMenuItem icon="home" iconSize="2x" >
                                <p className="m-4"> Kezdőlap</p>
                            </CDBSidebarMenuItem>
                        </Link>
                    </CDBSidebarMenu>
                    <CDBSidebarMenu>
                        <Link href="/students">
                            <CDBSidebarMenuItem icon="user-friends" iconSize="2x">
                                <p className="m-4"> Diákok</p>
                            </CDBSidebarMenuItem>
                        </Link>
                    </CDBSidebarMenu>
                    <CDBSidebarMenu>
                        <Link href="/workers">
                            <CDBSidebarMenuItem icon="user-tie" iconSize="2x" >
                              <p className="m-4"> Dolgozók</p>
                            </CDBSidebarMenuItem>
                        </Link>
                    </CDBSidebarMenu>
                    <CDBSidebarMenu>
                        <Link href="/notes">
                            <CDBSidebarMenuItem icon="user-edit" iconSize="2x">
                                <p className="m-4"> Feljegyzések</p>
                            </CDBSidebarMenuItem>
                        </Link>
                    </CDBSidebarMenu>
                    <CDBSidebarMenu>
                        <Link href="/mail">
                            <CDBSidebarMenuItem icon="envelope"  iconSize="2x">
                                <p className="m-4"> Üzenetek</p>
                            </CDBSidebarMenuItem>
                        </Link>
                    </CDBSidebarMenu>

                </CDBSidebarContent>
                <CDBSidebarFooter>
                    <Row className="mb-4">
                        <Col>
                            <Link href="/settings">
                                <CDBSidebarMenuItem icon="cog" iconSize="3x"></CDBSidebarMenuItem>
                            </Link>
                        </Col>
                        <Col>
                            <Link href="/login">
                                <CDBSidebarMenuItem icon="sign-out-alt" iconSize="3x" className="d-flex justify-content-between"></CDBSidebarMenuItem>
                            </Link>
                        </Col>
                    </Row>
                </CDBSidebarFooter>
            </CDBSidebar>
        </div>
    )
}