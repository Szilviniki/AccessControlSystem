export interface ITable {
     name: string;
     selector: (row: any) => any;
     sortable: boolean;


}[]