"use client";
import React, {ReactNode, useState} from 'react';
import TopNavigation from "@/components/Navbar/TopNavigation";
import {useCookies} from "next-client-cookies";
import SideMenu from "@/components/Navbar/Sidebar";

function Content({
    children
                 }: {
    children: ReactNode
    
}) {
    const cookies = useCookies();
    const [sidebarOpen, setSidebarOpen] = useState(cookies.get("sidebar") === "1")

    return (
        <>
            <SideMenu open={sidebarOpen}/>
            <div className={" pb-3 w-100 overflow-auto mb-4 position-relative"}>
                <TopNavigation setSidebar={(value:boolean) => {
                    setSidebarOpen(value);
                    cookies.set("sidebar", value ? "1" : "0");
                }} sidebar={sidebarOpen}/>
                <div className={"d-flex px-5 mt-5 pt-5"}>
                    {children}
                </div>
            </div>
        </>
    );
}

export default Content;