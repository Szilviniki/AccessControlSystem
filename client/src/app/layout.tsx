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
      <body className={inter.className}>
        {children}
      </body>
      </html>
  )
}
