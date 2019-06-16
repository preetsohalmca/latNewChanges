declare namespace Volvo.LAT.Models {

    /**
     * Defines a contract for the order tracking model.
     * It must be in sync with the OrderTrackingModel from C#
     */
    export interface OrderTrackingModel {
        Number: string;
        Status: string;
        OrderDate: Date;
        DeliveryDate?: Date;
        TotalPrice: number;
        IsRetrieveAuthorized: boolean;
        IsCancelAuthorized: boolean;
        IsConfirmAuthorized: boolean;
    }

}