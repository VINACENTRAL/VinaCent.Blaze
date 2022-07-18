using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinaCent.Blaze.Helpers
{
    public static class ComputerUnitHelper
    {
        public static string FromBytes(this object input, int decimals = 2)
        {
            if (input == null)
                return "0 Bytes";
            var bytes = double.Parse(input.ToString() ?? "0");
            if (bytes <= 0)
                return "0 Bytes";
            const int k = 1024;
            var dm = decimals < 0 ? 0 : decimals;
            var sizes = new[] { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            var i = (int)Math.Floor(Math.Log(bytes) / Math.Log(k));

            return $"{Math.Round(bytes / Math.Pow(k, i), dm)} {sizes[i]}";
        }
    }
}
