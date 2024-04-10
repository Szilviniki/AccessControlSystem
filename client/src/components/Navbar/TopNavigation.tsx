'use client'
import Navbar from "react-bootstrap/Navbar";
import {NavbarText} from "react-bootstrap";
import React from "react";
import {useCookies} from "next-client-cookies";
import {FaChevronLeft, FaChevronRight} from "react-icons/fa";

export default function TopNavigation({
                                          setSidebar,
    sidebar
                                      }: {
    setSidebar: any,
    sidebar: boolean
}) {
const cookies = useCookies();
    return(
        <Navbar className="Top-Nav" >
            <Navbar.Collapse className="justify-content-between ">
                <NavbarText className="mx-3 text-blacm" >
                    {sidebar ? (
                        <FaChevronLeft onClick={() => {
                            setSidebar(!sidebar)
                        }}/>
                    ) : (
                        <FaChevronRight onClick={() => {
                            setSidebar(!sidebar)
                        }}/>
                    )}
                </NavbarText>
                <NavbarText className="mx-3" >Jó napot {(useCookies().get("user-name")||"").replaceAll('"', '')}!</NavbarText>
            </Navbar.Collapse>
        </Navbar>
    )
}