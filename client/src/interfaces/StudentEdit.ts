export interface IStudentEdit{
    email : string[]|string
    name : string[]|string
    phone : string[]|string
    birthday : string[]|string
    error: boolean
    data?: null
    message? : string[]|string
    queryIsSuccess? : boolean
}