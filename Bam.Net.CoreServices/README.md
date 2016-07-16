# Core Services
- UserAccountsService
	- manage user accounts
- ApplicationService
	- manage application names, owners and api keys
- PersistenceService
	- manage data repositories.  Should use a CompositeRepository internally (future add Redis instance(s))
- ConfigurationService
	- manage application configurations by application name and base addresses
- LoggerService
	- duh

# Secondary Services
- TextService
	- manage text, alternative text and translations
- ListService
	- sub types: Cart, Shopping, Wish
