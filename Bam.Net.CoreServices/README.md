# Core Services
- UserAccountsService
	- manage user accounts
- ApiKeyService
	- manage application names, owners and api keys
- PersistenceService
	- manage data repositories.  
- ConfigurationService
	- manage application configurations by application name and base addresses
- PermissionsService
	- maintains acls for paths.  (Uses UserAccountsService)
- FileSystemService
	- maintain logical directories and files.  (Uses PermissionsService)
- StateService
	- track global state (key value pairs) and session state (key value pairs of key value pairs)
- LoggerService
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
