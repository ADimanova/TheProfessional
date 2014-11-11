using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Professional.Web.Helpers
{
    public static class StringManipulations
    {
        public static string GetSubstring(string item, int startIndex, int size)
        {
            if (item.Length > size)
            {
                return string.Format("{0}...", item.Substring(startIndex, size));
            }
            else
            {
                return item;
            }
        }

        public static string GetJoinedList(ICollection<string> items)
        {
            if (items.Count == 0)
            {
                throw new ArgumentException("You cannot get a joined string from empty collection");
            }
            return string.Join(", ", items);
        }
    }
}