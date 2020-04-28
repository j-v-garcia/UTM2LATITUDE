# UTM2LATSqlServerLibrary
A .NET class library for Converting WGS84/UTM coordinates to Latitude and Longitude using CoordinateSharp nuget library
This library contains utility methods that can be called from Microsoft SQL Server as SQL functions withn transact sql code (t-sql)


This project is based in SqlLibrary (https://github.com/rsingh85/sql-dotnet-library) coded by Ravinder Singh (https://github.com/rsingh85)
he wrote a blog explaining how to call a c sharp method from SQL Server: http://seesharpdeveloper.blogspot.com/2016/06/calling-c-method-from-sql-server.html






Extract from the file UTM2LATSqlServerLibrary.cs:
 ```csharp
/// <summary>
/// Converts UTM Zone 30 Polar Region South to Latitude and Longitude using CoordinateSharp nuget library
/// </summary>
/// <param name="XUTM">pos UTM X</param>
/// <param name="YUTM">pos UTM Y</param>
/// <param name="Hemisphere">Letter code. (see http://www.dmap.co.uk/utmworld.htm) </param>
/// <param name="Zone">Zone int value (see http://www.dmap.co.uk/utmworld.htm) </param>
/// <returns>Latitude</returns>
[SqlProcedure]
public static double UTM2LATITUDE(double XUTM, double YUTM, string Hemisphere, int Zone)
{

	UniversalTransverseMercator utm = new UniversalTransverseMercator(Hemisphere, Zone, XUTM, YUTM);
	Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);

	return c.Latitude.DecimalDegree;
}

/// <summary>
/// Converts UTM Zone 30 Polar Region South to Latitude and Longitude using CoordinateSharp nuget library
/// </summary>
/// <param name="XUTM">pos UTM X</param>
/// <param name="YUTM">pos UTM Y</param>
/// <param name="Hemisphere">Letter code. (see http://www.dmap.co.uk/utmworld.htm) </param>
/// <param name="Zone">Zone int value (see http://www.dmap.co.uk/utmworld.htm) </param>
/// <returns>Longitude</returns>
[SqlProcedure]
public static double UTM2LONGITUDE(double XUTM, double YUTM, string Hemisphere, int Zone)
{

	UniversalTransverseMercator utm = new UniversalTransverseMercator(Hemisphere, Zone, XUTM, YUTM);
	Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);

	return c.Longitude.DecimalDegree;
}
 ```


Usage:

1. Compile the project with release configuration Any CPU

3. Copy the compilation folder to your SQL Server

3. Run this t-sql commands. 
Note that to be able to run thist library you have to configure your database as trustworthy because how is the CoordinateSharp implemented.
This setting enables your database to run code marked as not safe. This has relation with the permissions of the assembly to access memory locations.
I wouldn't recommend to develop extensive assemblies with unsafe code because some error could end up messing your sql server memory with unknown results.
Nevertheless as we are using this for a simple task and it makes sense to me using this aproach.

 ```sql

USE [INSERT_DB_NAME_HERE]
GO

ALTER DATABASE [INSERT_DB_NAME_HERE] SET trustworthy ON
GO

CREATE ASSEMBLY UTM2LATSqlServerLibrary from '[INSERT_ASSEMBLY_PATH_HERE]\UTM2LATSqlServerLibrary.dll' WITH PERMISSION_SET = SAFE
GO

CREATE FUNCTION UTM2LATITUDE(@XUTM float, @YUTM float, @HEMISPHERE nvarchar(1), @ZONE int)
RETURNS [float] WITH EXECUTE AS CALLER
AS
EXTERNAL NAME [UTM2LATSqlServerLibrary].[UTM2LATSqlServerLibrary.UTM2LATSqlServerLibrary].[UTM2LATITUDE]
GO

CREATE FUNCTION UTM2LONGITUDE(@XUTM float, @YUTM float, @HEMISPHERE nvarchar(1), @ZONE int)
RETURNS [float] WITH EXECUTE AS CALLER
AS
EXTERNAL NAME [UTM2LATSqlServerLibrary].[UTM2LATSqlServerLibrary.UTM2LATSqlServerLibrary].[UTM2LONGITUDE]
GO

CREATE FUNCTION UTM2LAT(@XUTM float, @YUTM float)
RETURNS [float] WITH EXECUTE AS CALLER
AS
EXTERNAL NAME [UTM2LATSqlServerLibrary].[UTM2LATSqlServerLibrary.UTM2LATSqlServerLibrary].[UTM2LAT]
GO

CREATE FUNCTION UTM2LONG(@XUTM float, @YUTM float)
RETURNS [float] WITH EXECUTE AS CALLER
AS
EXTERNAL NAME [UTM2LATSqlServerLibrary].[UTM2LATSqlServerLibrary.UTM2LATSqlServerLibrary].[UTM2LONG]
GO

sp_configure 'clr enabled', 1
GO

RECONFIGURE
GO

 ```


4. You should have 4 functions in your database under Programmability/Functions/Scalar valued functions	

5. You can use the funcions like this:

 ```sql
 
SELECT dbo.UTM2LATITUDE(723399.51,4373328.5,'S',30) AS Latitude, dbo.UTM2LONGITUDE(723399.51,4373328.5,'S',30) AS Longitude
SELECT dbo.UTM2LAT(723399.51,4373328.5) AS Latitude, dbo.UTM2LONG(723399.51,4373328.5) AS Longitude

 ```



