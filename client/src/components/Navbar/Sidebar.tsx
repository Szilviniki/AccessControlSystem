'use client'
import {useCookies} from "next-client-cookies";
import {Menu, MenuItem, Sidebar, sidebarClasses, SubMenu} from "react-pro-sidebar";
import {FaBars, FaHome, FaUserEdit, FaUserFriends, FaUserTie} from "react-icons/fa";
import Link from "next/link";
import {FaHouseLock} from "react-icons/fa6";
import {usePathname} from "next/navigation";
import {Col, Row} from "react-bootstrap";
import {IoMdSettings} from "react-icons/io";

export default function SideMenu({
    open
                                  } : {
    open: boolean
}) {
    const cookies = useCookies();
    const path = usePathname();
    const isActive = (active: string) => {
        return active === path;
    }

    return (
        <Sidebar
            collapsed={!open}
            rootStyles={{
                [`.${sidebarClasses.container}`]: {
                    backgroundColor: "#006D77",
                    color: "#EDF6FF"
                },
            }}>
            < Menu>
                <Row className={"mb-4 mt-3"}>
                    <Col xs={12} className={"d-flex justify-content-center"}>
                        <FaHouseLock color="#EDF6FF" size={open?100:30} />
                    </Col>
                </Row>
                <MenuItem component={<Link href="/"/>} icon={<FaHome/>} active={isActive("/")}>Kezdőlap</MenuItem>
                <MenuItem component={<Link href="/students"/>} icon={<FaUserFriends/>} active={isActive("/students")}> Diákok </MenuItem>
                <MenuItem component={<Link href="/workers"/>} icon={<FaUserTie/>} active={isActive("/workers")}> Dolgozók </MenuItem>
                <MenuItem component={<Link href="/notes"/>} icon={<FaUserEdit/>} active={isActive("/notes")}> Feljegyzések </MenuItem>
                <MenuItem component={<Link href="/settings"/>} icon={<IoMdSettings/>} active={isActive("/settings")}> Beálítások </MenuItem>
                <MenuItem component={<Link href="/notes"/>} icon={<FaUserEdit/>} active={isActive("/notes")}> Feljegyzések </MenuItem>
            </Menu>
                
        </Sidebar>
    )
}
