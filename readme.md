
This project contains 2 powershell cmdlets to convert a dacpac into sql files in a folder structure, and back again.
The primary files used are 
DacpacSqlConverter.cs (for converting the dacpac into files)
SqlDacpacConverter.cs (for converting them back)

This project uses SqlServer 140, so please reference those assemblies

They can be used in powershell as below

#import the module
Import-Module .\bin\Debug\DacpacSqlConverter.dll

#convert dacpac into files
Format-DacpacAsSql -InputPath "C:/dacpac/staging.dacpac" -OutputDirectory "C:/dacpac/"

#convert them back to dacpac
Format-SqlAsDacpac -FileDirectory "C:/dacpac" -OutputPath "C:/dacpac/test2.dacpac"