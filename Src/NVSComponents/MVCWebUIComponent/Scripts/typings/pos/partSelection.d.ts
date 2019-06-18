declare namespace Volvo.LAT.Models {

    /**
     * Defines a contract for the part selection model.
     * It must be in sync with the PartSelection from C#
     */
    export interface PartSelection {
        Number: number;
        Name: string;
        Availability: number;
        Price: number;
    }

}