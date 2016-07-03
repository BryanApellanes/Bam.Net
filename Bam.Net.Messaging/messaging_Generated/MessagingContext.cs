/*
	This file was generated and should not be modified directly
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Messaging.Data
{
	// schema = Messaging 
    public static class MessagingContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Messaging";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class EmailMessageQueryContext
	{
			public EmailMessageCollection Where(WhereDelegate<EmailMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.EmailMessage.Where(where, db);
			}
		   
			public EmailMessageCollection Where(WhereDelegate<EmailMessageColumns> where, OrderBy<EmailMessageColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Messaging.Data.EmailMessage.Where(where, orderBy, db);
			}

			public EmailMessage OneWhere(WhereDelegate<EmailMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.EmailMessage.OneWhere(where, db);
			}

			public static EmailMessage GetOneWhere(WhereDelegate<EmailMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.EmailMessage.GetOneWhere(where, db);
			}
		
			public EmailMessage FirstOneWhere(WhereDelegate<EmailMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.EmailMessage.FirstOneWhere(where, db);
			}

			public EmailMessageCollection Top(int count, WhereDelegate<EmailMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.EmailMessage.Top(count, where, db);
			}

			public EmailMessageCollection Top(int count, WhereDelegate<EmailMessageColumns> where, OrderBy<EmailMessageColumns> orderBy, Database db = null)
			{
				return Bam.Net.Messaging.Data.EmailMessage.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<EmailMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.EmailMessage.Count(where, db);
			}
	}

	static EmailMessageQueryContext _emailMessages;
	static object _emailMessagesLock = new object();
	public static EmailMessageQueryContext EmailMessages
	{
		get
		{
			return _emailMessagesLock.DoubleCheckLock<EmailMessageQueryContext>(ref _emailMessages, () => new EmailMessageQueryContext());
		}
	}
	public class DirectMessageQueryContext
	{
			public DirectMessageCollection Where(WhereDelegate<DirectMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.DirectMessage.Where(where, db);
			}
		   
			public DirectMessageCollection Where(WhereDelegate<DirectMessageColumns> where, OrderBy<DirectMessageColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Messaging.Data.DirectMessage.Where(where, orderBy, db);
			}

			public DirectMessage OneWhere(WhereDelegate<DirectMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.DirectMessage.OneWhere(where, db);
			}

			public static DirectMessage GetOneWhere(WhereDelegate<DirectMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.DirectMessage.GetOneWhere(where, db);
			}
		
			public DirectMessage FirstOneWhere(WhereDelegate<DirectMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.DirectMessage.FirstOneWhere(where, db);
			}

			public DirectMessageCollection Top(int count, WhereDelegate<DirectMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.DirectMessage.Top(count, where, db);
			}

			public DirectMessageCollection Top(int count, WhereDelegate<DirectMessageColumns> where, OrderBy<DirectMessageColumns> orderBy, Database db = null)
			{
				return Bam.Net.Messaging.Data.DirectMessage.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DirectMessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.DirectMessage.Count(where, db);
			}
	}

	static DirectMessageQueryContext _directMessages;
	static object _directMessagesLock = new object();
	public static DirectMessageQueryContext DirectMessages
	{
		get
		{
			return _directMessagesLock.DoubleCheckLock<DirectMessageQueryContext>(ref _directMessages, () => new DirectMessageQueryContext());
		}
	}
	public class MessageQueryContext
	{
			public MessageCollection Where(WhereDelegate<MessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.Message.Where(where, db);
			}
		   
			public MessageCollection Where(WhereDelegate<MessageColumns> where, OrderBy<MessageColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Messaging.Data.Message.Where(where, orderBy, db);
			}

			public Message OneWhere(WhereDelegate<MessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.Message.OneWhere(where, db);
			}

			public static Message GetOneWhere(WhereDelegate<MessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.Message.GetOneWhere(where, db);
			}
		
			public Message FirstOneWhere(WhereDelegate<MessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.Message.FirstOneWhere(where, db);
			}

			public MessageCollection Top(int count, WhereDelegate<MessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.Message.Top(count, where, db);
			}

			public MessageCollection Top(int count, WhereDelegate<MessageColumns> where, OrderBy<MessageColumns> orderBy, Database db = null)
			{
				return Bam.Net.Messaging.Data.Message.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<MessageColumns> where, Database db = null)
			{
				return Bam.Net.Messaging.Data.Message.Count(where, db);
			}
	}

	static MessageQueryContext _messages;
	static object _messagesLock = new object();
	public static MessageQueryContext Messages
	{
		get
		{
			return _messagesLock.DoubleCheckLock<MessageQueryContext>(ref _messages, () => new MessageQueryContext());
		}
	}    }
}																								
