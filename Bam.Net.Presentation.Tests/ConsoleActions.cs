using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using Bam.Net.CommandLine;
using Bam.Net.Data.SQLite;
using Bam.Net.Data.Repositories;
using Bam.Net.Presentation.Unicode;
using System.IO;
using NCuid;
using System.Drawing;
using System.Drawing.Imaging;
using Bam.Net.Presentation.Svg;

namespace Bam.Net.Presentation.Tests
{
    [Serializable]
    public class ConsoleActions: CommandLineTestInterface
    {
        [ConsoleAction]
        public void InkscapeTests()
        {
            Inkscape.ExportSvg("C:\\bam\\img-ws\\empty_logo.svg", 3600, 3600);
        }

        [ConsoleAction]
        public void WriteEmojiPngFiles()
        {
            SQLiteDatabase db = new SQLiteDatabase(".", "Emojis");
            EmojiCollection emojis = Emoji.LoadAll(db);
            string rootDir = "C:\\bam\\content\\common\\img\\emojis\\png";
            string header = "data:image/png;base64,";
            foreach (Emoji emoji in emojis)
            {
                if (emoji.Windows.StartsWith(header))
                {
                    string base64 = emoji.Windows.TruncateFront(header.Length);
                    string filePath = Path.Combine(rootDir, $"{emoji.ShortName.Trim().Replace(" ", "-").Replace("~", "").Replace("#", "").Replace("%", "").Replace("&", "").Replace("*", "").Replace("{", "").Replace("}", "").Replace(":", "").Replace("\\", "")}.png");
                    byte[] data = base64.FromBase64();
                    OutLineFormat("Writing {0}", ConsoleColor.Green, filePath);
                    File.WriteAllBytes(filePath, data);
                }
            }
        }

        [ConsoleAction]
        public void ScrapeEmojis()
        {
            CQ cq = CQ.Create(GetEmojiHtml());
            SQLiteDatabase db = new SQLiteDatabase("c:\\bam\\temp", "Emojis");
            db.TryEnsureSchema<Emoji>();
            var tables = cq["table"];
            var rows = cq["tr", tables[0]];

            OutLineFormat("There are {0} rows", rows.Length.ToString());
            Category category = new Category();
            rows.Each(tr =>
            {
                var cells = cq["td", tr];
                var header = cq["th", tr];
                
                if(header.Length == 1)
                {
                    category = new Category()
                    {
                        Cuid = Cuid.Generate(),
                        Name = cq["a", header].Text()
                    };
                    OutLineFormat("New Category {0}", ConsoleColor.Cyan, category.Name);
                    category.Save(db);
                }
                if (cells.Length == 15)
                {
                    Emoji emoji = new Emoji()
                    {
                        CategoryId = category.Id,
                        Cuid = Cuid.Generate(),
                        Number = int.Parse(cells[0].InnerText),
                        Character = cells[2].InnerText,
                        Apple = GetImageData(cq, cells[3]),
                        Google = GetImageData(cq, cells[4]),
                        Twitter = GetImageData(cq, cells[5]),
                        One = GetImageData(cq, cells[6]),
                        Facebook = GetImageData(cq, cells[7]),
                        Samsung = GetImageData(cq, cells[8]),
                        Windows = GetImageData(cq, cells[9]),
                        GMail = GetImageData(cq, cells[10]),
                        SoftBank = GetImageData(cq, cells[11]),
                        DoCoMo = GetImageData(cq, cells[12]),
                        KDDI = GetImageData(cq, cells[13]),
                        ShortName = cells[14].InnerText.Trim()
                    };
                    emoji.Save(db);
                    Expect.IsTrue(emoji.Id > 0);
                    string codes = cq["a", cells[1]].Text();
                    string[] codeArray = codes.DelimitSplit(" ");
                    foreach(string value in codeArray)
                    {
                        Code code = emoji.CodesByEmojiId.AddNew();
                        code.EmojiId = emoji.Id;
                        code.Value = value;
                        code.Save(db);
                    }
                    OutLineFormat("Saved {0}: {1}", ConsoleColor.Green, emoji.ShortName, emoji.Character);
                }
            });
        }

        private string GetImageData(CQ cq, IDomObject cell)
        {
            if (!cell.HasClass("miss"))
            {
                var img = cq["img", cell];
                if (img != null)
                {
                    return img.Attr("src");
                }
            }
            return "-";
        }

        string fullHtml = string.Empty;
        private string GetEmojiHtml()
        {
            if (string.IsNullOrEmpty(fullHtml))
            {
                fullHtml = File.ReadAllText("c:\\bam\\temp\\full-emoji-list.html"); // downloaded from http://unicode.org/emoji/charts/full-emoji-list.html
            }
            return fullHtml;
        }

    }
}
