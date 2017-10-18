using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Presentation;
using Bam.Net.Drawing;
using System.IO;

namespace Bam.Net.Yaml
{
    public static class PaletteExtensions
    {
        public static void AddYamlLoader()
        {
            ColorPalette.AddLoader(".yaml", (path) => File.ReadAllText(path).FromYaml<ColorPalette>());
        }

        public static void AddYamlSaver(this ColorPalette palette)
        {
            palette.AddSaver(".yaml", (pal, path, overwrite) => pal.ToYaml().SafeWriteToFile(path, overwrite));
        }
    }
}
