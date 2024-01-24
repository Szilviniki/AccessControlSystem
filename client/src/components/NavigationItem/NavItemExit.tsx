'use client'
import { Nav } from "react-bootstrap";
import { useCookies } from "next-client-cookies";
import { useRouter } from "next/navigation";
import React from "react";

export default function NavItemExit() {
    const router = useRouter();
    const cookies = useCookies();

    const exit = () => {
        cookies.remove("email");
        router.replace('/login');
    };

    return (
        <Nav.Link eventKey="exit" onClick={exit} className="Nav-Item">
            <img src='/images/box-arrow.svg' alt="Kilépés kép" />
        </Nav.Link>
    );
}
