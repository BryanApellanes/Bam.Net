/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Bam.Net.ServiceProxy
{
    public interface IResponse
    {
        // Summary:
        //     Gets or sets the System.Text.Encoding for this response's System.Net.HttpListenerResponse.OutputStream.
        //
        // Returns:
        //     An System.Text.Encoding object suitable for use with the data in the System.Net.HttpListenerResponse.OutputStream
        //     property, or null if no encoding is specified.
        Encoding ContentEncoding { get; set; }
        //
        // Summary:
        //     Gets or sets the number of bytes in the body data included in the response.
        //
        // Returns:
        //     The value of the response's Content-Length header.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     The value specified for a set operation is less than zero.
        //
        //   System.InvalidOperationException:
        //     The response is already being sent.
        //
        //   System.ObjectDisposedException:
        //     This object is closed.
        long ContentLength64 { get; set; }
        //
        // Summary:
        //     Gets or sets the MIME type of the content returned.
        //
        // Returns:
        //     A System.String instance that contains the text of the response's Content-Type
        //     header.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The value specified for a set operation is null.
        //
        //   System.ArgumentException:
        //     The value specified for a set operation is an empty string ("").
        //
        //   System.ObjectDisposedException:
        //     This object is closed.
        string ContentType { get; set; }
        //
        // Summary:
        //     Gets or sets the collection of cookies returned with the response.
        //
        // Returns:
        //     A System.Net.CookieCollection that contains cookies to accompany the response.
        //     The collection is empty if no cookies have been added to the response.
        CookieCollection Cookies { get; set; }
        //
        // Summary:
        //     Gets or sets the collection of header name/value pairs returned by the server.
        //
        // Returns:
        //     A System.Net.WebHeaderCollection instance that contains all the explicitly
        //     set HTTP headers to be included in the response.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Net.WebHeaderCollection instance specified for a set operation
        //     is not valid for a response.
        WebHeaderCollection Headers { get; set; }
        //
        // Summary:
        //     Gets or sets a value indicating whether the server requests a persistent
        //     connection.
        //
        // Returns:
        //     true if the server requests a persistent connection; otherwise, false. The
        //     default is true.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     This object is closed.
        bool KeepAlive { get; set; }
        //
        // Summary:
        //     Gets a System.IO.Stream object to which a response can be written.
        //
        // Returns:
        //     A System.IO.Stream object to which a response can be written.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     This object is closed.
        Stream OutputStream { get; }
                //
        // Summary:
        //     Gets or sets the value of the HTTP Location header in this response.
        //
        // Returns:
        //     A System.String that contains the absolute URL to be sent to the client in
        //     the Location header.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The value specified for a set operation is an empty string ("").
        //
        //   System.ObjectDisposedException:
        //     This object is closed.
        string RedirectLocation { get; set; }
        //
        // Summary:
        //     Gets or sets whether the response uses chunked transfer encoding.
        //
        // Returns:
        //     true if the response is set to use chunked transfer encoding; otherwise,
        //     false. The default is false.
        bool SendChunked { get; set; }
        //
        // Summary:
        //     Gets or sets the HTTP status code to be returned to the client.
        //
        // Returns:
        //     An System.Int32 value that specifies the HTTP status code for the requested
        //     resource. The default is System.Net.HttpStatusCode.OK, indicating that the
        //     server successfully processed the client's request and included the requested
        //     resource in the response body.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     This object is closed.
        //
        //   System.Net.ProtocolViolationException:
        //     The value specified for a set operation is not valid. Valid values are between
        //     100 and 999 inclusive.
        int StatusCode { get; set; }
        //
        // Summary:
        //     Gets or sets a text description of the HTTP status code returned to the client.
        //
        // Returns:
        //     The text description of the HTTP status code returned to the client. The
        //     default is the RFC 2616 description for the System.Net.HttpListenerResponse.StatusCode
        //     property value, or an empty string ("") if an RFC 2616 description does not
        //     exist.
        string StatusDescription { get; set; }

        // Summary:
        //     Closes the connection to the client without sending a response.
        void Abort();
        //
        // Summary:
        //     Adds the specified header and value to the HTTP headers for this response.
        //
        // Parameters:
        //   name:
        //     The name of the HTTP header to set.
        //
        //   value:
        //     The value for the name header.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     name is null or an empty string ("").
        //
        //   System.ArgumentException:
        //     You are not allowed to specify a value for the specified header.-or-name
        //     or value contains invalid characters.
        //
        //   System.ArgumentOutOfRangeException:
        //     The length of value is greater than 65,535 characters.
        void AddHeader(string name, string value);
        //
        // Summary:
        //     Adds the specified System.Net.Cookie to the collection of cookies for this
        //     response.
        //
        // Parameters:
        //   cookie:
        //     The System.Net.Cookie to add to the collection to be sent with this response
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     cookie is null.
        void AppendCookie(Cookie cookie);
        //
        // Summary:
        //     Appends a value to the specified HTTP header to be sent with this response.
        //
        // Parameters:
        //   name:
        //     The name of the HTTP header to append value to.
        //
        //   value:
        //     The value to append to the name header.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     name is null or an empty string ("").-or-You are not allowed to specify a
        //     value for the specified header.-or-name or value contains invalid characters.
        //
        //   System.ArgumentOutOfRangeException:
        //     The length of value is greater than 65,535 characters.
        void AppendHeader(string name, string value);
        //
        // Summary:
        //     Sends the response to the client and releases the resources held by this
        //     System.Net.HttpListenerResponse instance.
        void Close();
        //
        // Summary:
        //     Returns the specified byte array to the client and releases the resources
        //     held by this System.Net.HttpListenerResponse instance.
        //
        // Parameters:
        //   responseEntity:
        //     A System.Byte array that contains the response to send to the client.
        //
        //   willBlock:
        //     true to block execution while flushing the stream to the client; otherwise,
        //     false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     responseEntity is null.
        //
        //   System.ObjectDisposedException:
        //     This object is closed.
        void Close(byte[] responseEntity, bool willBlock);
        //
        // Summary:
        //     Configures the response to redirect the client to the specified URL.
        //
        // Parameters:
        //   url:
        //     The URL that the client should use to locate the requested resource.
        void Redirect(string url);
        //
        // Summary:
        //     Adds or updates a System.Net.Cookie in the collection of cookies sent with
        //     this response.
        //
        // Parameters:
        //   cookie:
        //     A System.Net.Cookie for this response.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     cookie is null.
        //
        //   System.ArgumentException:
        //     The cookie already exists in the collection and could not be replaced.
        void SetCookie(Cookie cookie);
    }
}
