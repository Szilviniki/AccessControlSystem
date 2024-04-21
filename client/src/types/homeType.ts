import logType from "@/types/logType";
import noteType from "@/types/noteType";

type home = {
    presentStudents:number,
    absentStudents:number,
    noteCount:number,
    lastLogs:logType[],
    notes:noteType[]
}
export default home
