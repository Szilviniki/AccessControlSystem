import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import 'bootstrap/dist/css/bootstrap.min.css';
import './globals.css'
import { Container } from 'react-bootstrap'


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
      <head>
      </head>
      <body className={inter.className}>
      <Container>
        {children}
      </Container>
      </body>
      </html>
  )
}
