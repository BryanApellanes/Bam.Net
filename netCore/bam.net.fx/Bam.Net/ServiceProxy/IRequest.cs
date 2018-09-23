/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Bam.Net.ServiceProxy
{
    public interface IRequest
    {
        string[] AcceptTypes { get; set; }
        Encoding ContentEncoding { get; set; }
        //
        // Summary:
        //     Gets the length of the body data included in the request.
        //
        // Returns:
        //     The value from the request's Content-Length header. This value is -1 if the
        //     content length is not known.
        long ContentLength64 { get; }

        int ContentLength { get; }

        NameValueCollection QueryString { get; }
        //
        // Summary:
        //     Gets the MIME type of the body data included in the request.
        //
        // Returns:
        //     A System.String that contains the text of the request's Content-Type header.
        string ContentType { get; }
        //
        // Summary:
        //     Gets the cookies sent with the request.
        //
        // Returns:
        //     A System.Net.CookieCollection that contains cookies that accompany the request.
        //     This property returns an empty collection if the request does not contain
        //     cookies.
        CookieCollection Cookies { get; }
        
        //
        // Summary:
        //     Gets the collection of header name/value pairs sent in the request.
        //
        // Returns:
        //     A System.Net.WebHeaderCollection that contains the HTTP headers included
        //     in the request.
        NameValueCollection Headers { get; }
        //
        // Summary:
        //     Gets the HTTP method specified by the client.
        //
        // Returns:
        //     A System.String that contains the method used in the request.
        string HttpMethod { get; }
        //
        // Summary:
        //     Gets a stream that contains the body data sent by the client.
        //
        // Returns:
        //     A readable System.IO.Stream object that contains the bytes sent by the client
        //     in the body of the request. This property returns System.IO.Stream.Null if
        //     no data is sent with the request.
        Stream InputStream { get; }

        //
        // Summary:
        //     Gets the System.Uri object requested by the client.
        //
        // Returns:
        //     A System.Uri object that identifies the resource requested by the client.
        Uri Url { get; }
        //
        // Summary:
        //     Gets the Uniform Resource Identifier (URI) of the resource that referred
        //     the client to the server.
        //
        // Returns:
        //     A System.Uri object that contains the text of the request's System.Net.HttpRequestHeader.Referer
        //     header, or null if the header was not included in the request.
        Uri UrlReferrer { get; }
        //
        // Summary:
        //     Gets the user agent presented by the client.
        //
        // Returns:
        //     A System.String object that contains the text of the request's User-Agent
        //     header.
        string UserAgent { get; }
        //
        // Summary:
        //     Gets the server IP address and port number to which the request is directed.
        //
        // Returns:
        //     A System.String that contains the host address information.
        string UserHostAddress { get; }
        //
        // Summary:
        //     Gets the DNS name and, if provided, the port number specified by the client.
        //
        // Returns:
        //     A System.String value that contains the text of the request's Host header.
        string UserHostName { get; }
        //
        // Summary:
        //     Gets the natural languages that are preferred for the response.
        //
        // Returns:
        //     A System.String array that contains the languages specified in the request's
        //     System.Net.HttpRequestHeader.AcceptLanguage header or null if the client
        //     request did not include an System.Net.HttpRequestHeader.AcceptLanguage header.
        string[] UserLanguages { get; }

        //
        // Summary:
        //     Gets the URL information (without the host and port) requested by the client.
        //
        // Returns:
        //     A System.String that contains the raw URL for this request.
        string RawUrl { get; }
    }
}
