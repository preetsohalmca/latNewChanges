declare namespace Volvo.LAT.Models {

    /**
     * Defines a contract for the part selection model.
     * It must be in sync with the PartSelection from C#
     */
    export interface PoSelection {
        Number: number;
        RequesterName: string;
        LineItemDescription: string;
        Availability: number;
        PoNumber: number;
        ContractStartDate: Date;
        ContractEndDate: Date;
        PoLine: Int32Array;
    }

}