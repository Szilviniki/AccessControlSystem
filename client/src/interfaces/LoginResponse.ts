export interface ILoginResponse{
    email? : string[]|string
    name? : string[]|string
    role? : string[]|string
    id? : string[]|string
    token? : string[]|string
    error?: boolean
    data?: null
    message? : string[]|string
    queryIsSuccess? : boolean
}