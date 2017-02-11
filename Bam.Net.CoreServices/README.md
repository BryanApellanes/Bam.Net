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
