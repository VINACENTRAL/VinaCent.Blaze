using System.IO;
using System.Linq;

namespace VinaCent.Blaze.Helpers
{
    public static class ExtensionCheckerHelper
    {
        public static readonly string[] ImageExtensions =
        {
            ".ase",
            ".art",
            ".bmp",
            ".blp",
            ".cd5",
            ".cit",
            ".cpt",
            ".cr2",
            ".cut",
            ".dds",
            ".dib",
            ".djvu",
            ".egt",
            ".exif",
            ".gif",
            ".gpl",
            ".grf",
            ".icns",
            ".ico",
            ".iff",
            ".jng",
            ".jpeg",
            ".jpg",
            ".jfif",
            ".jp2",
            ".jps",
            ".lbm",
            ".max",
            ".miff",
            ".mng",
            ".msp",
            ".nitf",
            ".ota",
            ".pbm",
            ".pc1",
            ".pc2",
            ".pc3",
            ".pcf",
            ".pcx",
            ".pdn",
            ".pgm",
            ".PI1",
            ".PI2",
            ".PI3",
            ".pict",
            ".pct",
            ".pnm",
            ".pns",
            ".ppm",
            ".psb",
            ".psd",
            ".pdd",
            ".psp",
            ".px",
            ".pxm",
            ".pxr",
            ".qfx",
            ".raw",
            ".rle",
            ".sct",
            ".sgi",
            ".rgb",
            ".int",
            ".bw",
            ".tga",
            ".tiff",
            ".tif",
            ".vtf",
            ".xbm",
            ".xcf",
            ".xpm",
            ".3dv",
            ".amf",
            ".ai",
            ".awg",
            ".cgm",
            ".cdr",
            ".cmx",
            ".dxf",
            ".e2d",
            ".egt",
            ".eps",
            ".fs",
            ".gbr",
            ".odg",
            ".svg",
            ".stl",
            ".vrml",
            ".x3d",
            ".sxd",
            ".v2d",
            ".vnd",
            ".wmf",
            ".emf",
            ".art",
            ".xar",
            ".png",
            ".webp",
            ".jxr",
            ".hdp",
            ".wdp",
            ".cur",
            ".ecw",
            ".iff",
            ".lbm",
            ".liff",
            ".nrrd",
            ".pam",
            ".pcx",
            ".pgf",
            ".sgi",
            ".rgb",
            ".rgba",
            ".bw",
            ".int",
            ".inta",
            ".sid",
            ".ras",
            ".sun",
            ".tga"
        };

        public static readonly string[] AudioExtensions =
        {
            ".aac",
            ".aiff",
            ".ape",
            ".au",
            ".flac",
            ".gsm",
            ".it",
            ".m3u",
            ".m4a",
            ".mid",
            ".mod",
            ".mp3",
            ".mpa",
            ".pls",
            ".ra",
            ".s3m",
            ".sid",
            ".wav",
            ".wma",
            ".xm"
        };

        public static string GetExtension(this string input)
        {
            input = input.Trim();
            if (!input.StartsWith("."))
            {
                input = Path.GetExtension(input);
            }

            input = input.ToLower();
            return input;
        }

        public static bool IsImage(this string input)
        {
            input = input.GetExtension();
            return ImageExtensions.Any(x => x.ToLower() == input);
        }

        public static bool IsAudio(this string input)
        {
            input = input.GetExtension();
            return AudioExtensions.Any(x => x.ToLower() == input);
        }
    }
}
