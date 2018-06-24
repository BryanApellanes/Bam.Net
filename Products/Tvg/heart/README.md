# Heart

Heart.exe provides the core set of services for Bam.Net.

## UserRegistryService

UserRegistryService implements the interfaces IUserManager, IUserResolver and IRoleResolver.

## ConfigurationService
ConfigurationService implements the interface IConfigurationService.

## OAuthService (not implemented)

## FileService

## SystemLoggerService

SystemLoggerService implements ILog and ILogEventEmitter.  For an ILogger implementation that
logs to the SystemLoggerService use Bam.Net.Services.Clients.CoreLoggerClient.

## NotificationService

NotificationService implements the interface INotficationService.