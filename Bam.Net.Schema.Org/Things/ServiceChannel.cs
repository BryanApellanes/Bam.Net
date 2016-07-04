using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A means for accessing a service, e.g. a government office location, web site, or phone number.</summary>
	public class ServiceChannel: Intangible
	{
		///<summary>A language someone may use with the item. Please use one of the language codes from the IETF BCP 47 standard. See also inLanguage.</summary>
		public OneOfThese<Language , Text> AvailableLanguage {get; set;}
		///<summary>Estimated processing time for the service using this channel.</summary>
		public Duration ProcessingTime {get; set;}
		///<summary>The service provided by this channel.</summary>
		public Service ProvidesService {get; set;}
		///<summary>The location (e.g. civic structure, local business, etc.) where a person can go to access the service.</summary>
		public Place ServiceLocation {get; set;}
		///<summary>The phone number to use to access the service.</summary>
		public ContactPoint ServicePhone {get; set;}
		///<summary>The address for accessing the service by mail.</summary>
		public PostalAddress ServicePostalAddress {get; set;}
		///<summary>The number to access the service by text message.</summary>
		public ContactPoint ServiceSmsNumber {get; set;}
		///<summary>The website to access the service.</summary>
		public URL ServiceUrl {get; set;}
	}
}
