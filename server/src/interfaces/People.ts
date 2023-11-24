interface IFacutly {
    group_id?:number,
    name:string,
    present:boolean,
    password:string,
    phone?:string,
    email?:string,
    role_id?:number,
    username:string,
}

interface IGroup {
    leader_id?:number,
    group_name:number,
}

interface IStudent {
    group_id:number,
    age:number,
    name:string,
    is_in:boolean,
    phone?:string,
    email?:string,
}

interface IParent {
    id:number,
    name:string,
    phone?:string,
    email:string,
    is_primary:boolean
}

export {IParent, IGroup, IStudent, IFacutly}