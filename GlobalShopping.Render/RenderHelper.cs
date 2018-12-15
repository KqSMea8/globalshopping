using System;

namespace GlobalShopping.Render
{
    public class RenderHelper
    {
        public static string GetContentType(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return "text/html";
            }
            string cleanurl = url.ToLower();
            int QuestionMark = url.IndexOf("?");
            if (QuestionMark > 0)
            {
                cleanurl = url.Substring(0, QuestionMark);
            }
            cleanurl = cleanurl.Trim('\t', '\r', '\n');
            string extension = System.IO.Path.GetExtension(cleanurl);

            var contentType = "text/html";
            if (!string.IsNullOrEmpty(extension))
            {
                if (extension.ToLower() == ".txt")
                {
                    contentType = "application/octet-stream";
                }
                else
                {
                    contentType = MimeMapping.MimeUtility.GetMimeMapping(extension);
                }
            }

            return contentType;

        }
        public static UrlFileType GetFileType(string url, string ContentType = "")
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return UrlFileType.Unknow;
            }
            string cleanurl = url.ToLower();
            int QuestionMark = url.IndexOf("?");
            if (QuestionMark > 0)
            {
                cleanurl = url.Substring(0, QuestionMark);
            }
            cleanurl = cleanurl.Trim('\t', '\r', '\n');
            string extension = System.IO.Path.GetExtension(cleanurl);

            if (!string.IsNullOrWhiteSpace(extension))
            {
                if (extension == ".js")
                {
                    return UrlFileType.JavaScript;
                }
                else if (extension == ".css")
                {
                    return UrlFileType.Style;
                }

                if (extension == ".jpg" || extension == ".ico" || extension == ".gif" || extension == ".bmp" || extension == ".png" || extension == ".jpeg" || extension == ".svg")
                {
                    return UrlFileType.Image;
                }

                if (extension == ".aspx" || extension == ".axd" || extension == ".asx" || extension == ".ashx" || extension == ".asmx" || extension == ".asp" || extension == ".cfm" || extension == ".yaws" || extension == ".html" || extension == ".htm" || extension == ".shtml" || extension == ".xhtml" || extension == ".jhtml" || extension == ".cshtml")
                {
                    return UrlFileType.Html;
                }

                if (extension == ".jsp" || extension == ".jspx" || extension == ".wss" || extension == ".do" || extension == ".action" || extension == ".pl" || extension == ".php" || extension == ".php3" || extension == ".php4" || extension == ".phtml" || extension == ".py" || extension == ".cgi" || extension == ".dll" || extension == ".rb" || extension == ".rhtml" )
                {
                    return UrlFileType.Html;
                }

                if (extension == ".swf" || extension == ".flv" || extension == ".mid" || extension == ".midi" || extension == ".mp3" || extension == ".mpg" || extension == ".mpeg" || extension == ".mov" || extension == ".rar" || extension == ".zip" || extension == ".7zip" || extension == ".wav" || extension == ".tiff" || extension == ".txt")
                {
                    return UrlFileType.File;
                }

                if (extension == ".doc" || extension == ".docx" || extension == ".ppt" || extension == ".pptx" || extension == ".xls" || extension == ".xlsx" || extension == ".pdf" || extension == ".pps")
                {
                    return UrlFileType.File;
                }
            }

            if (!string.IsNullOrWhiteSpace(ContentType))
            {
                ContentType = ContentType.ToLower();

                if (ContentType.Contains("image"))
                {
                    return UrlFileType.Image;
                }

                if (ContentType.Contains("text"))
                {
                    if (ContentType.Contains("css") || ContentType.Contains("style"))
                    {
                        return UrlFileType.Style;
                    }

                    if (ContentType.Contains("script"))
                    {
                        return UrlFileType.JavaScript;
                    }
                    else
                    {
                        return UrlFileType.Html;
                    }
                }
                else if (ContentType.Contains("application"))
                {
                    return UrlFileType.File;
                }
            }

            if (string.IsNullOrEmpty(extension))
            {
                return UrlFileType.Html;
            }

            return UrlFileType.File;

        }

        public static string CombinePath(string Root, string RelativeUrl)
        {
            if (!Root.EndsWith("\\"))
            {
                Root = Root + "\\";
            }

            if (string.IsNullOrEmpty(RelativeUrl))
            {
                return Root;
            }
            RelativeUrl = RelativeUrl.Replace("/", "\\");
            if (RelativeUrl.StartsWith("\\"))
            {
                RelativeUrl = RelativeUrl.Substring(1);
            }
            return System.IO.Path.Combine(Root, RelativeUrl);
        }

        public static string RemoveQuestionMark(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            int mark = input.IndexOf("?");
            if (mark > 0)
            {
                return input.Substring(0, mark);
            }
            return input;
        }
    }
    public enum UrlFileType
    {
        Unknow = 0,
        Image = 1,
        JavaScript = 2,
        Style = 3,
        File = 4,
        Html = 5
    }
}
