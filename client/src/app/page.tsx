import MainTemplate from "@/app/templates/MainTemplate";
import {cookies} from "next/headers";
import {redirect} from "next/navigation";
import HomePageForm from "@/components/Home/HomeForm";

export default function HomePage() {
    const user = cookies().get("user-name");
    if (!user) {
        redirect("/login")
    }
    else {
    return(
        <>
        <MainTemplate>
            <HomePageForm/>
        </MainTemplate>
        </>
    )}
}
