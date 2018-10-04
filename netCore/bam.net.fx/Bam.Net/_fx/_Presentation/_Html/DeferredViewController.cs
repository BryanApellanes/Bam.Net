/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using System.Web.Mvc;
using System.Threading;

namespace Bam.Net.Presentation.Html
{
    public class DeferredViewController: Controller
    {
        static Dictionary<string, DeferredView> _views;

        static DeferredViewController()
        {
            _views = new Dictionary<string, DeferredView>();
            MaxViewAge = new TimeSpan(0, 8, 0);
        }

        static DeferredView _default;
        static object _defaultLock = new object();
        public static DeferredView DefaultView
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new DeferredView("Default", (dv) => new Tag("span").Attr("name", "defaultDeferred")));
            }
        }

        static Timer _cleanupTimer;
        static TimeSpan _maxViewAge;
        public static TimeSpan MaxViewAge
        {
            get
            {
                return _maxViewAge;
            }
            set
            {
                _maxViewAge = value;
                _cleanupTimer = new Timer((o) =>
                {
                    CleanUp();
                }, null, value, value);
            }
        }

        public ActionResult GetContent(string name, bool reload = false)
        {
            DeferredView view = GetView(name);
            if (view == null)
            {
                return Json(new { Data = "error", Success = false, Message = "Deferred View {0} was not found"._Format(name), ContentReady = true }); // ContentReady to prevent further attempts to retrieve content
            }

            return Json(new { Data = GetContentString(name, reload), Success = true, ContentReady = view.ContentReady });
        }

        public static string GetContentString(string name, bool reload = false)
        {
            string value = DefaultView.Content.ToHtmlString();
            if (_views.ContainsKey(name))
            {
                DeferredView view = _views[name];
                if (reload)
                {
                    value = view.ContentProvider(view).ToHtmlString();
                }
                else
                {
                    value = view.ContentReady ? view.Content.ToHtmlString() : view.State.ToHtmlString();
                }
            }

            return value;
        }

        public static DeferredView GetView(string viewName)
        {
            DeferredView result = null;
            if (_views.ContainsKey(viewName))
            {
                result = _views[viewName];
            }

            return result;
        }

        public static void SetView(DeferredView view)
        {
            if (_views.ContainsKey(view.Name))
            {
                _views[view.Name] = view;
            }
            else
            {
                _views.Add(view.Name, view);
            }
        }

        static bool _cleaning;
        public static void CleanUp()
        {
            if (!_cleaning)
            {
                _cleaning = true;
                DateTime now = DateTime.Now;                
                string[] keys = _views.Keys.ToArray();
                for (int i = 0; i < keys.Length; i++)
                {
                    string key = keys[i];
                    DeferredView view = _views[key];
                    TimeSpan age = now.Subtract(view.Created);
                    if (age > MaxViewAge)
                    {
                        _views.Remove(key);
                    }
                }
                _cleaning = false;
            }
        }
    }
}
