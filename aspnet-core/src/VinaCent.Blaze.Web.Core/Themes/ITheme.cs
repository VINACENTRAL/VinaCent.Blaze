using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinaCent.Blaze.Themes
{
    public interface ITheme
    {
        string GetLayout(string name, bool fallbackToDefault = true);
    }
}
