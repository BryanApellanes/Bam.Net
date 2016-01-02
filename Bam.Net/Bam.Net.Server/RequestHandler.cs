/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net;
using Bam.Net.Incubation;
using System.IO;
using System.Net;
using Bam.Net.Html;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server
{
	//public class RequestHandler: IRequestHandler
	//{
	//	List<IResponder> _responders;
	//	ILogger _logger;

	//	public event Action<IRequestHandler, IResponder> ResponderAdded;

	//	public RequestHandler(BamConf conf, ILogger logger = null, bool setResponders = true)
	//	{
	//		this._logger = logger ?? Log.Default;
	//		this._responders = new List<IResponder>();

	//		this.BamConf = conf;

	//		//if (setResponders)
	//		//{
	//		//	this.SetResponders();
	//		//}
	//	}

	//	public ILogger Logger
	//	{
	//		get
	//		{
	//			return _logger;
	//		}
	//	}

		//public void SetResponders()
		//{
		//	this.AddResponder(Content);
		//	this.AddResponder(Dao);
		//	this.AddResponder(ServiceProxy);
		//}
        
		//ServiceProxyResponder _serviceProxy;
		//public ServiceProxyResponder ServiceProxy
		//{
		//	get
		//	{
		//		if (_serviceProxy == null)
		//		{
		//			_serviceProxy = new ServiceProxyResponder(BamConf, Logger, this);
		//		}

		//		return _serviceProxy;
		//	}
		//}

		//DaoResponder _dao;
		//public DaoResponder Dao
		//{
		//	get
		//	{
		//		if (_dao == null)
		//		{
		//			_dao = new DaoResponder(BamConf, Logger, this);
		//		}
		//		return _dao;
		//	}
		//}

		//ContentResponder _content;
		//public ContentResponder Content
		//{
		//	get
		//	{
		//		if (_content == null)
		//		{
		//			_content = new ContentResponder(BamConf, Logger, this);
		//		}

		//		return _content;
		//	}
		//}

		//internal Fs Fs
		//{
		//	get { return BamConf.Fs; }
		//	private set
		//	{
		//		BamConf.Fs = value;
		//	}
		//}

		//public BamConf BamConf
		//{
		//	get;
		//	set;
		//}

		///// <summary>
		///// Add an IResponder implementation to this
		///// request handler
		///// </summary>
		///// <param name="responder"></param>
		//public void AddResponder(IResponder responder)
		//{
		//	this._responders.Add(responder);
		//	if (ResponderAdded != null)
		//	{
		//		ResponderAdded(this, responder);
		//	}
		//}

		//public void RemoveResonder(IResponder responder)
		//{
		//	if (_responders.Contains(responder))
		//	{
		//		_responders.Remove(responder);
		//	}
		//}

		//public IResponder[] Responders
		//{
		//	get
		//	{
		//		return _responders.ToArray();
		//	}
		//}

		//Action<IRequest, IResponse> _responderNotFoundHandler;
		//object _responderNotFoundHandlerLock = new object();
		///// <summary>
		///// Get or set the default handler used when no appropriate
		///// responder is found for a given request.  This is the 
		///// Action responsible for responding with a 404 status code
		///// and supplying any additional information to the client.
		///// </summary>
		//public Action<IRequest, IResponse> ResponderNotFoundHandler
		//{
		//	get
		//	{
		//		return _responderNotFoundHandlerLock.DoubleCheckLock(ref _responderNotFoundHandler, () => HandleResponderNotFound);
		//	}
		//	set
		//	{
		//		_responderNotFoundHandler = value;
		//	}
		//}

		//Action<IRequest, IResponse, Exception> _exceptionHandler;
		//object _exceptionHandlerLock = new object();
		///// <summary>
		///// Get or set the default exception handler.  This is the
		///// Action responsible for responding with a 500 status code
		///// and supplying any additional information to the client
		///// pertaining to exceptions that may occur on the server.
		///// </summary>
		//public Action<IRequest, IResponse, Exception> ExceptionHandler
		//{
		//	get
		//	{
		//		return _exceptionHandlerLock.DoubleCheckLock(ref _exceptionHandler, () => HandleException);
		//	}
		//	set
		//	{
		//		_exceptionHandler = value;
		//	}
		//}
        
		//#region IRequestHandler Members

		//public void HandleRequest(IHttpContext context)
		//{            
		//	IRequest request = context.Request;
		//	IResponse response = context.Response;
		//	IResponder responder = new ResponderList(BamConf, _responders);
		//	try
		//	{
		//		if (!responder.Respond(context))
		//		{
		//			HandleResponderNotFound(request, response);
		//		}
		//		else
		//		{
		//			response.StatusCode = (int)HttpStatusCode.OK;
		//			response.OutputStream.Flush();
		//			response.OutputStream.Close();                    
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleException(request, response, ex);
		//	}
		//}

		//private void HandleResponderNotFound(IRequest request, IResponse response)
		//{
		//	string path = request.Url.ToString();
		//	string messageFormat = "No responder was found for the path: {0}";
		//	string description = "Responder not found";

		//	using (StreamWriter sw = new StreamWriter(response.OutputStream))
		//	{
		//		response.StatusCode = (int)HttpStatusCode.NotFound;
		//		response.StatusDescription = description;
		//		sw.WriteLine("<!DOCTYPE html>");
		//		Tag html = new Tag("html");
		//		html.Child(new Tag("body")
		//			.Child(new Tag("h1").Text(description))
		//			.Child(new Tag("p").Text(string.Format(messageFormat, path)))
		//		);
		//		sw.WriteLine(html.ToHtmlString());
		//		sw.Flush();
		//		sw.Close();
		//	}

		//	Logger.AddEntry(messageFormat, LogEventType.Warning, path);
		//}

		//private void HandleException(IRequest request, IResponse response, Exception ex)
		//{
		//	using (StreamWriter sw = new StreamWriter(response.OutputStream))
		//	{
		//		string description = "({0})"._Format(ex.Message);
		//		response.StatusCode = (int)HttpStatusCode.InternalServerError;
		//		response.StatusDescription = description;
		//		sw.WriteLine("<!DOCTYPE html>");
		//		Tag html = new Tag("html");
		//		html.Child(new Tag("body")
		//			.Child(new Tag("h1").Text("Internal Server Exception"))
		//			.Child(new Tag("p").Text(description))
		//		);
		//		sw.WriteLine(html.ToHtmlString());
		//		sw.Flush();
		//		sw.Close();
		//	}

		//	Logger.AddEntry("An error occurred handling the request: ({0})\r\n*** Request Details ***\r\n{1}",
		//			ex,
		//			ex.Message,
		//			request.PropertiesToString());
		//}

	//	#endregion
	//}
}
