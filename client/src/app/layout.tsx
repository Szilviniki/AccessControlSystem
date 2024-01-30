import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import React from "react";
import {CookiesProvider} from "next-client-cookies/server";
import {Col, Container, Row} from "react-bootstrap";
import VerticalNavigation from "@/components/VerticalNavigation";
import TopNavigation from "@/components/TopNavigation";


const inter = Inter({ subsets: ['latin'] })

export const metadata: Metadata = {
   title:"Access Control System",
}

export default function RootLayout({
                                     children,
                                   }: {
  children: React.ReactNode
}) {
  return (
      <html lang="en">
          <body className={inter.className}>
              <CookiesProvider>
                  <Container fluid>
                      {children}
                  </Container>
              </CookiesProvider>
          </body>
      </html>
  )
}
