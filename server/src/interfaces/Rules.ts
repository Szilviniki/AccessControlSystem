interface IRole {
    name:string,
    can_edit_privilege?:boolean,
    can_edit_student_list?:boolean,
    can_approve_guest?:boolean,
    can_write_messages?:boolean
}
interface IPrivilege {
    type:number,
    name:string
    description?:string,
}

interface IPunishment {
    name:string,
    type:number,
    description?:string
}