# Core Services
- CoreUserManagerService
	- manage user accounts
- CoreApplicationRegistryService
	- manage application names, owners and api keys
- CoreConfigurationService
	- manage application configurations by application name and base addresses
- CoreLoggerService
	- duh

# Persistence Services
- CorePersistenceService [todo]
	- manage data repositories.  
- CorePermissionsService [todo]
	- maintains acls for paths.  (Uses UserAccountsService)
- CoreFileSystemService [todo]
	- maintain logical directories and files.  (Uses PermissionsService)
- CoreStateService [todo]
	- track global state (key value pairs) and session state (key value pairs of key value pairs)

# Secondary Services
- TextService
	- manage text, alternative text and translations
- ListService
	- sub types: Cart, Shopping, Wish
- PubSubHubService
	- event subscription and notification
