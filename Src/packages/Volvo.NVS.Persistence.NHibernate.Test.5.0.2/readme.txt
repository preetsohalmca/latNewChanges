Getting Started

  This package (Volvo.NVS.Persistence.NHibernate.Test) is designed to help the creation of tests decoupled 
from external databases. It uses the LocalDB in-file database to create easily and quickly a simple 
test scenario.

How to use:

Having a class already designed, mapped and with their repository created, use the following method:

   LocalDbTestHelper.SetupDatabase(typeof(<YourClassHere>).Assembly, "dbFileName");

You are ready to run your tests!

  The "SetupDatabase" method creates the database, opens the connection, finds and sets up the mappings and exports 
the schema (create the database according with the found mappings).

For more information, please refer to the Persitence Library samples and documentation at:
   
https://teamplace.volvo.com/sites/volvoit-dotNET/SAD/Persistence.aspx

