// use-client.page.tsx
'use client'
import { useRouter } from "next/navigation";
import LoginForm from "@/components/LoginFrom";
import { FormValues } from "@/components/LoginFrom";
import LoginLayout from "@/app/LoginLayout";
import { Col, Row, Image } from "react-bootstrap";
import Notiflix from "notiflix";

export default function Login() {
    const router = useRouter()

    const handleFormSubmit = async (values: FormValues): Promise<void> => {
        try {
            console.log(values);
            if (!values.username || !values.password) {
                console.log("Hiányzó felhasználónév vagy jelszó");
                Notiflix.Report.warning(
                    'Minden mezőt ki kell tölteni!',
                    'Figyeljen oda, hogy minden mező legyen kitöltve',
                    'Rendben',
                );
            } else {
                router.push('/home');
            }
        } catch (error) {
            console.error("Hiba történt:", error);
        }
    };

    return (
        <LoginLayout>
            <Row className=" justify-content-center ">
                <Col sm={6} md={12} lg={6}>
                    <Image src="images/person-circle.svg"
                           alt="itt lenne a kép"
                           id="loginImage" />
                </Col>
            </Row>
            <Row className=" justify-content-center ">
                <Col sm={5} md={6}>
                    <LoginForm
                                 inputs={[
                                     {
                                         id: "username",
                                         label: "Email cím",
                                         type: "text"
                                     }, {
                                         id: "password",
                                         label: "Jelszó",
                                         type: "password",
                                     }, {
                                         id: "submit",
                                         label: "Belépés",
                                         type: "button",
                                     }
                                 ]}
                                 onSubmitFunction={handleFormSubmit}
                    />
                </Col>
            </Row>
        </LoginLayout>
    );
}
