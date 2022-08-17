using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinaCent.Blaze.AppCore.FileUnits
{
    public static class FileUnitConsts
    {
        /// <summary>
        /// Physical directory to store system files
        /// </summary>
        public const string ContentPhysicalDirectory = "contents";

        /// <summary>
        /// Virtual root directory to contains all user files
        /// </summary>
        public const string UsersDirName = "personal";

        /// <summary>
        /// Virtual root directory to contain all system's runtime files
        /// </summary>
        public const string ContentsDirName = "contents";

        /// <summary>
        /// Static
        /// </summary>
        public static readonly string[] StaticDirName = { UsersDirName, ContentsDirName };
    }
}
