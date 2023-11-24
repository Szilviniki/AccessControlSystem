interface IMessage {
    subject:string,
    message:string,
    sent_on:Date,
    is_important?:boolean
}

interface ILogs {
    person_id:number,
    is_guest:Boolean
    timestamp:Date
}

export {ILogs, IMessage}