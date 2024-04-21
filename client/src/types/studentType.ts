import note from "@/types/noteType";
import parentType from "@/types/parentType";

type studentType = {
    id:string|undefined,
    name:string|undefined,
    email:string|undefined,
    birthDate:Date|undefined,
    phone:string|undefined,
    isPresent:boolean,
    notes:note[],
    parent:parentType
}

export default studentType