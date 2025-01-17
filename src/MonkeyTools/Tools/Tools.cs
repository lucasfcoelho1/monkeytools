﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MonkeyTools
{
    public static partial class Tools
    {
        public static bool IsMatch(string stringValue, string pattern)
        {
            if (string.IsNullOrEmpty(stringValue))
                return false;
            return Regex.IsMatch(stringValue, pattern, RegexOptions.IgnoreCase);
        }

        public static string ReplaceAll(string stringValue, List<char> oldChars, List<char> newChars)
        {
            if (string.IsNullOrEmpty(stringValue) || oldChars == null || newChars == null)
                return stringValue;
            var builder = new StringBuilder(stringValue);
            foreach (var c in oldChars)
                builder.Replace(c, newChars[oldChars.FindIndex(cc => cc == c)]);
            return builder.ToString();
        }

        public static string RemoveAccents(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
                return stringValue;

            string lst1 = "áéíóúàèìòùäëïöüãõâêîôûçñÁÉÍÓÚÀÈÌÒÙÄËÏÖÜÃÕÂÊÎÔÛÇÑ";
            string lst2 = "aeiouaeiouaeiouaoaeioucnAEIOUAEIOUAEIOUAOAEIOUCN";
            return ReplaceAll(stringValue, lst1.ToCharArray().ToList(), lst2.ToCharArray().ToList());
        }

        public static string RemoveCase(string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
                return string.Empty;
            return RemoveAccents(stringValue.ToLower());
        }

        public static string Concat<t>(this IEnumerable<t> items, Func<t, object> valueFunction, string separator = ", ", string format = null, string defaultValue = null, bool distinct = true)
        {
            if (items == null || !items.Any())
                return defaultValue;
            var newlst = (distinct ? items.Select(item => valueFunction(item)).Distinct() : items.Select(item => valueFunction(item))).ToList();
            var str = new StringBuilder();
            foreach (var value in newlst)
            {
                var valuestr = Convert.ToString(value);
                if (!string.IsNullOrEmpty(valuestr))
                    str.Append(string.IsNullOrEmpty(format) ? valuestr + separator : string.Format("{0:" + format + "}", value) + separator);
            }
            return string.IsNullOrEmpty(str.ToString()) ? null : str.Remove(str.Length - separator.Length, separator.Length).ToString();
        }

        public static string GetContentType(string filename, bool isExtension = false)
        {
            var ext = isExtension ? filename : Path.GetExtension(filename);
            switch (ext.ToLower())
            {
                case ".pdf": return "application/pdf";
                case ".zip": return "application/zip";
                case ".js": return "application/javascript";
                case ".gif": return "image/gif";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".ico": return "image/x-icon";
                case ".tif": return "image/tiff";
                case ".tiff": return "image/tiff";
                case ".eml": return "message/rfc822";
                case ".mp4": return "video/mp4";
                case ".mp3": return "audio/mpeg";
                case ".mov": return "video/quicktime";
                case ".mpg": return "video/mpeg";
                case ".avi": return "video/x-msvideo";
                case ".wmv": return "video/x-ms-wmv";
                case ".xls": return "application/vnd.ms-excel";
                case ".doc": return "application/msword";
                case ".ppt": return "application/vnd.ms-powerpoint";
                case ".pps": return "application/vnd.ms-powerpoint";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".xltx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
                case ".dotx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
                case ".ppsx": return "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
                case ".rtf": return "application/rtf";
                case ".css": return "text/css";
                case ".csv": return "text/csv";
                case ".txt": return "text/plain";
                case ".xml": return "text/xml";
                case ".htm": return "text/html";
                case ".html": return "text/html";
            }
            return "application/octet-stream";
        }

    }
}
