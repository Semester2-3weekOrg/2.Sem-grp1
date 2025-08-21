using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Helpers
{
    internal static class InitialsConverter
    {
        public static string GetInitialsFromName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            return new string(name.Where(char.IsUpper).ToArray());
        }
    }
}
