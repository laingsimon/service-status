# service-status

Owin-Self hosted application which can return the status of services via HTTP Rest

## Usage

All usages of this service are via HTTP, on port 2016. There are no restrictions to whom can access the service - no authentication is required.
To protect against unwanted usage; ensure firewalls are setup correctly so that connections are only permitted from acceptable sources.

## Windows services

`GET http://[server]:2016/WindowsService/<service>`

e.g.

`GET http://localhost:2016/WindowsService/LanmanWorkstation`

Will return an object such as:

```
{
	"name": "LanmanWorkstation",
	"status": "Running"
}
```

The status value is a name from the [ServiceControllerStatus](https://msdn.microsoft.com/en-us/library/system.serviceprocess.servicecontrollerstatus.aspx) enum in the .net framework.

**NOTE** This service exposes the possibility of enumerating services installed on a server; although it does not permit control of them, it might expose possible vulnurabilities on the machine. Ensure that firewalls, or other tools, are employed to ensure only legitate sources can query the service.

## Database schema

`GET http://[server]:2016/SchemaVersion/<database>`

e.g.

`GET http://localhost:2016/SchemaVersion/Northwind`

Will return an object such as:

```
{
	"major": 1,
	"minor": 1,
	"version": "1.1"
}
```

This requires that the service is installed and running as a user whom is able to connect to the SQL server (on the same server) via Windows Authentication.
The database must contain a database table called 'Version' in the 'Meta' schema. This table must have numerical columns called MajorSchemaVersion and MinorSchemaVersion.

**NOTE** This service attempts to protect against SQL injection as best as possible; however it would be best to ensure that this service is limited to only those whom have a legitate reason for using it.
It would also be wise to ensure that the user that is used/created for this service has sufficiently restricted permissions.

## Installation

The service can be installed on a server by following the following steps:

1. Build the solution in `Release` configuration
2. Copy all files in the `bin\Release` directory to a directory on the desired server
3. Execute `ServiceStatus.exe /install` from the directory where the files were copied
4. Update the start parameter for the services control panel; execute `services.msc` to see them
5. Start the service as appropriate

## Uninstallation

Execute `ServiceStatus.exe /uninstall`, the service should be stopped prior to running the command.