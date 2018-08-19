# Core Services

Core services are common to all bam applications and are hosted at bamapps.net.

## UserRegistryService

The UserRegistryService manages user accounts for a bam application.  It implements Bam.Net.UserAccounts.IUserManagerwhich is 
useful for signing up and messaging new users.

## ApplicationRegistrationService

The ApplicationRegistrationService manages applications, their names, their owners and their api keys.

## CoreConfigurationService

The ConfigurationService manages application configuration settings.

## CoreLoggerService

A central implementation of ILogger.

# Persistence Services
- PersistenceService [todo]
	- manage data repositories.  
- FileSystemService [todo]
	- maintain logical directories and files.  (Uses PermissionsService)
- StateService [todo]
	- track global state (key value pairs) and session state (key value pairs of key value pairs)

# Other Services
- PermissionsService [todo]
	- maintains acls for paths.  (Uses CoreUserAccountsService)
- TextService
	- manage text, alternative text and translations
- ListService
	- sub types: Inventory, Cart, Shopping, Wish
- EventHubService
	- event subscription and notification
