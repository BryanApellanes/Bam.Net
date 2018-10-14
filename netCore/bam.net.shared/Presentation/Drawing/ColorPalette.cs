/*
	Copyright © Bryan Apellanes 2015  
*/
using BoneSoft.CSS;
using CsQuery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;

namespace Bam.Net.Drawing
{
    [Serializable]
    public class ColorPalette
    {
        Dictionary<string, Action<ColorPalette, string, bool>> _savers;
        static Dictionary<string, Func<string, ColorPalette>> _loaders;
        
        SortedDictionary<string, HexColor> _colorsByName;
        static ColorPalette()
        {
            _loaders = new Dictionary<string, Func<string, ColorPalette>>();
            _loaders.Add(".json", (path) =>
            {
                return new FileInfo(path).FromJson<ColorPalette>();
            });

            _loaders.Add(".xml", (path) =>
            {
                return File.ReadAllText(path).FromXml<ColorPalette>();
            });
        }

        public static void AddLoader(string extension, Func<string, ColorPalette> loader)
        {
            _loaders.Add(extension, loader);
        }

        public void AddSaver(string extension, Action<ColorPalette, string, bool> saver)
        {
            _savers.Add(extension, saver);
        }

        public ColorPalette()
        {
            this._colorsByName = new SortedDictionary<string, HexColor>();
            this._savers = new Dictionary<string, Action<ColorPalette, string, bool>>();
            this._savers.Add(".json", (pal, path, overwrite) =>
            {
                pal.ToJson().SafeWriteToFile(path, overwrite);
            });

            this._savers.Add(".xml", (pal, path, overwrite) =>
            {
                pal.ToXml().SafeWriteToFile(path, overwrite);
            });            
        }

        [XmlIgnore]
        public HexColor this[string colorName]
        {
            get
            {
                if (this._colorsByName.ContainsKey(colorName))
                {
                    return this._colorsByName[colorName];
                }
                else
                {
                    return null;
                }
            }
        }

        public HexColor[] Colors
        {
            get { return this._colorsByName.Values.ToArray(); }
            set
            {
                this._colorsByName.Clear();
                foreach (HexColor color in value)
                {
                    this._colorsByName.Add(color.Name, color);
                }
            }
        }

        public void Add(string hexColor)
        {
            Add(hexColor, hexColor);
        }

        /// <summary>
        /// Adds the specified color if one with the same name
        /// has not already been added.
        /// </summary>
        /// <param name="colorName"></param>
        /// <param name="hexColor"></param>
        public void Add(string colorName, string hexColor)
        {
            HexColor color = new HexColor(colorName, hexColor);
            Add(color);
        }

        public virtual void Add(HexColor color)
        {
            Expect.IsTrue(IsValidHexColor(color.Hex));
            if (!_colorsByName.ContainsKey(color.Name))
            {
                _colorsByName.Add(color.Name, color);
            }
        }

        public void Set(string colorName, string hexColor)
        {
            HexColor color = new HexColor(colorName, hexColor);
            Set(color);
        }

        public void Set(HexColor color)
        {
            Expect.IsTrue(IsValidHexColor(color.Hex));
            if (!_colorsByName.ContainsKey(color.Name))
            {
                _colorsByName.Add(color.Name, color);
            }
            else
            {
                _colorsByName[color.Name] = color;
            }
        }

        static List<char> hexChars = new List<char>(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' });
        public static bool IsValidHexColor(string hexColorStringToValidate)
        {
            hexColorStringToValidate = hexColorStringToValidate.Trim().ToUpper();

            if (hexColorStringToValidate.Length != 7)
            {
                return false;
            }

            if (!hexColorStringToValidate.StartsWith("#"))
            {
                return false;
            }

            List<char> characters = new List<char>(hexColorStringToValidate.ToCharArray());
            characters.RemoveAt(0);
            foreach (char character in characters)
            {
                if (!hexChars.Contains(character))
                {
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Remove the color with the specified name if it exists
        /// </summary>
        /// <param name="colorName"></param>
        public void Remove(string colorName)
        {
            if (this._colorsByName.ContainsKey(colorName))
            {
                this._colorsByName.Remove(colorName);
            }
        }

        public virtual void Save(string filePath, bool overwrite = false)
        {
            string ext = Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(ext) || !_loaders.ContainsKey(ext))
            {
                ext = ".yaml";
            }

            _savers[ext](this, filePath, overwrite);
        }

        public static ColorPalette Load(string filePath)
        {
            string ext = Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(ext) || !_loaders.ContainsKey(ext))
            {
                ext = ".yaml";
            }

            return _loaders[ext](filePath);
        }

        private static WebClient GetWebClient()
        {
            WebClient client = new WebClient();
            client.Headers["User-Agent"] =
        "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
        "(compatible; MSIE 6.0; Windows NT 5.1; " +
        ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            return client;
        }

        /// <summary>
        /// Derive a ColorPalette from the specified Uri by 
        /// finding colors in all the linked stylesheets
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static ColorPalette DeriveFrom(Uri url)
        {
            string ext = Path.GetExtension(url.AbsolutePath);
            ColorPalette result = new ColorPalette();
            if (!ext.ToLowerInvariant().Equals(".css"))
            {
                WebClient wc = GetWebClient();
                string html = wc.DownloadString(url.ToString());
                CQ cq = CQ.Create(html);
                cq["head"].Children("link[rel=stylesheet]").Each((i, dollarSign) =>
                {
                    string href = cq[dollarSign].Attr("href");
                    string path = string.Format("{0}://{1}{2}", url.Scheme, url.Authority, href);
                    List<HexColor> current = new List<HexColor>();
                    ColorPalette palette = DeriveFrom(path);
                    if (href.StartsWith("//"))
                    {
                        href = string.Format("http:{0}", href);
                    }
                    ColorPalette palette2 = DeriveFrom(href);
                    current.AddRange(palette.Colors);
                    current.AddRange(palette2.Colors);

                    foreach (HexColor color in current)
                    {
                        result.Add(color);
                    }
                });

                cq["head"].Children("style").Each((i, dollarSign) =>
                {
                    ColorPalette palette = DeriveFromCss(cq[dollarSign].Text());
                    foreach (HexColor color in palette.Colors)
                    {
                        result.Add(color);
                    }
                });
            }
            else
            {
                result = DeriveFrom(url.ToString());
            }

            return result;
        }

        /// <summary>
        /// Derives a color pallette from the specified css url
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ColorPalette DeriveFrom(string path)
        {
            try
            {
                string css = string.Empty;
                if (path.StartsWith("http://") || path.StartsWith("https://"))
                {
                    WebClient wc = GetWebClient();
                    css = wc.DownloadString(path);
                }
                else
                {
                    css = File.ReadAllText(path);
                }

                return DeriveFromCss(css);
            }
            catch (Exception ex)
            {
                Logging.Log.AddEntry("Error deriving color palette from : ({0})", ex, path);
                return new ColorPalette();
            }
        }

        private static ColorPalette DeriveFromCss(string css)
        {
            CSSParser parser = new CSSParser();
            CSSDocument cssDoc = parser.ParseText(css);
            ColorPalette palette = new ColorPalette();

            Action<Term> termAction = (t) =>
            {
                if (t.Type == TermType.Hex)
                {
                    palette.Add(t.ToString());
                }
            };

            Action<Declaration> declarationAction = (d) =>
            {
                if (d.Expression != null)
                {
                    foreach (Term term in d.Expression.Terms)
                    {
                        termAction(term);
                    }
                }
            };

            Action<RuleSet> rulesetAction = (r) =>
            {
                ForEachDeclarationIn(r, declarationAction);
            };

            ForEachRuleSetIn(cssDoc, rulesetAction);

            return palette;
        }

        private static void ForEachRuleSetIn(IRuleSetContainer rsc, Action<RuleSet> action)
        {
            foreach (RuleSet rs in rsc.RuleSets)
            {
                action(rs);
            }
        }

        private static void ForEachDeclarationIn(IDeclarationContainer dc, Action<Declaration> action)
        {
            foreach (Declaration decl in dc.Declarations)
            {
                action(decl);
            }
        }
    }
}
