# Core Services
- CoreUserManagerService
	- manage user accounts
- CoreApplicationRegistryService
	- manage application names, owners and api keys
- CorePersistenceService [todo]
	- manage data repositories.  
- CoreConfigurationService
	- manage application configurations by application name and base addresses
- CorePermissionsService [todo]
	- maintains acls for paths.  (Uses UserAccountsService)
- CoreFileSystemService [todo]
	- maintain logical directories and files.  (Uses PermissionsService)
- CoreStateService [todo]
	- track global state (key value pairs) and session state (key value pairs of key value pairs)
- CoreLoggerService
	- duh

# Secondary Services
- TextService
	- manage text, alternative text and translations
- ListService
	- sub types: Cart, Shopping, Wish
- PubSubHubService
	- event subscription and notification
- QueueService
	- use rabbitmq to request long running processing
