declare namespace Volvo.LAT.Models {

    /**
    * Defines a contract for the user model.
    * It must be in sync with the UserModel from C#
    */
    export interface UserModel {

        UserName: string;
        FirstName: string;
        LastName: string;
        LatestLogin?: Date;
        Role: RoleModel;
        
    }

}