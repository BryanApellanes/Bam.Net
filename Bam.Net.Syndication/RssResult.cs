/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Argotic.Common;
using Argotic.Syndication;
using Bam.Net.Syndication.Attributes;

namespace Bam.Net.Syndication
{
    public class RssResult: ActionResult
    {
        public RssResult(RssFeed feed)
        {
            this.Feed = feed;
        }

        public RssResult(object channel)
        {
            RssFeed f = new RssFeed();

            Type type = SetTitle(channel, f);

            string link = (string)GetValue<LinkAttribute>(channel);
            if (!string.IsNullOrEmpty(link))
            {
                f.Channel.Link = new Uri(link);
            }
            string description = (string)GetValue<DescriptionAttribute>(channel);
            f.Channel.Description = (string)GetValue<DescriptionAttribute>(channel);

            PropertyInfo itemsProp = type.GetFirstProperyWithAttributeOfType<ItemsAttribute>();
            object items = itemsProp.GetValue(channel, null);

            IEnumerable rssItems = new object[] { items };
            if (items is IEnumerable)
            {
                rssItems = (IEnumerable)items;
            }

            foreach (object item in rssItems)
            {
                AddItem(f, item);
            }

            this.Feed = f;
        }

        private void AddItem(RssFeed f, object item)
        {
            RssItem rssItem = new RssItem();
            rssItem.Title = (string)GetValue<TitleAttribute>(item);
            rssItem.Author = (string)GetValue<AuthorAttribute>(item);
            rssItem.Description = (string)GetValue<DescriptionAttribute>(item);
            rssItem.PublicationDate = (DateTime)GetValue<PublicationDateAttribute>(item);
            rssItem.Link = new Uri((string)GetValue<LinkAttribute>(item));
            f.Channel.AddItem(rssItem);
        }

        private static Type SetTitle(object channel, RssFeed f)
        {
            Type type = channel.GetType();
            ChannelAttribute channelAttr = null;

            if (type.HasCustomAttributeOfType<ChannelAttribute>(out channelAttr))
            {
                if (!string.IsNullOrEmpty(channelAttr.Title))
                {
                    f.Channel.Title = channelAttr.Title;
                }
            }

            if(string.IsNullOrEmpty(f.Channel.Title))
            {
                PropertyInfo prop = type.GetFirstProperyWithAttributeOfType<TitleAttribute>();
                if (prop != null)
                {
                    f.Channel.Title = (string)prop.GetValue(channel, null);
                }
                else
                {
                    f.Channel.Title = channel.ToString();
                }
            }

            return type;
        }

        private object GetValue<T>(object item) where T : Attribute, new()
        {
            return GetValue(item.GetType().GetFirstProperyWithAttributeOfType<T>(), item);
        }

        private object GetValue(PropertyInfo prop, object item)
        {
            if (prop == null)
            {
                return null;
            }

            return prop.GetValue(item, null);
        }

        public RssFeed Feed { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";
            SyndicationResourceSaveSettings settings = new SyndicationResourceSaveSettings();
            settings.CharacterEncoding = new UTF8Encoding(false);
            Feed.Save(context.HttpContext.Response.OutputStream, settings);
        }
    }
}
