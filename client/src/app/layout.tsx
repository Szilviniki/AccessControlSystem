import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import React from "react";
import {CookiesProvider} from "next-client-cookies/server";
import { Container} from "react-bootstrap";


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
      <html lang="hu">
          <body className={inter.className}>
              <CookiesProvider>
                  <Container fluid className="p-0 w-100" style={{height: "100vh"}}>
                      {children}
                  </Container>
              </CookiesProvider>
          </body>
      </html>
  )
}
