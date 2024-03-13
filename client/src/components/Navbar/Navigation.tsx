"use client"
import React, {useState} from 'react';
import SideMenu2 from "@/components/Navbar/Sidebar2";

function Navigation({
    sidebar
                    } : {
    sidebar: boolean
}) {
    return (
        <>
            <SideMenu2 open={sidebar}/>
        </>
    );
}

export default Navigation;