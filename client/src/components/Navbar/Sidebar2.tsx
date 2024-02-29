'use client'
import {useCookies} from "next-client-cookies";
import {Menu, MenuItem, Sidebar, sidebarClasses, SubMenu} from "react-pro-sidebar";
import {FaBars, FaHome, FaUserEdit, FaUserFriends, FaUserTie} from "react-icons/fa";
import Link from "next/link";
import {FaHouseLock} from "react-icons/fa6";

export default function SideMenu2() {
    const cookies=useCookies();

    return (
        <Sidebar
            rootStyles={{
                [`.${sidebarClasses.container}`]: {
                    backgroundColor: "#006D77",
                    color:"#EDF6FF"
                },
            }}>
            < Menu
                menuItemStyles = { {
                    button : {
                    [ `&.active` ] : {
                        backgroundColor : "black" ,
                        szín : "red" , }
                    , }
                    , }
                } >
                <FaHouseLock  color="#EDF6FF" size="75%" className="m-2"/>
                {/** <SubMenu label="Charts">
                    <MenuItem> Line charts </MenuItem>
            </SubMenu>**/}
                <MenuItem component={<Link href="/" /> } active={true} icon={<FaHome /> } >Kezdőlap</MenuItem>
                <MenuItem component={<Link href="/students" />} icon={<FaUserFriends />}> Diákok </MenuItem>
                <MenuItem component={<Link href="/workers" />} icon={<FaUserTie />} > Dolgozók </MenuItem>
                <MenuItem component={<Link href="/notes" />} icon={<FaUserEdit />}> Feljegyzések </MenuItem>
            </Menu>
        </Sidebar>
    )
}
