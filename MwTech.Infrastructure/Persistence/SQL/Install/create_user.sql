-- zakładanie użytkownika

CREATE LOGIN User1 
    WITH PASSWORD = 'alamakota';
GO

-- Creates a database user for the login created above.
CREATE USER User1 FOR LOGIN User1;
GO

/*
lub ręcznie
object explorer
	-> Centurion1 (SQL Server 11.0.3128 - centurion1\mariusz)
			-> security
				-> logins (right click -> new login)

założony login musi mieć w zakładce "server roles" ustawione public i sysadmin


SQL Server Configuration Manager
	SQL Server Network Configuration
		Protocols for MSSQLSERVER
			TCP/IP => Enabled

*/