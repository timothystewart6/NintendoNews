using System;

namespace AppStudio.Data
{
    /// <summary>
    /// Implementation of the StaticSchema.
    /// </summary>
    public class HtmlSchema : BindableSchemaBase
    {
        private string _content;

        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public override string DefaultTitle
        {
            get { return null; }
        }

        public override string DefaultSummary
        {
            get { return Content; }
        }

        public override string DefaultImageUrl
        {
            get { return null; }
        }

        override public string GetValue(string fieldName)
        {
            if (!String.IsNullOrEmpty(fieldName))
            {
                switch (fieldName.ToLower())
                {
                    case "id":
                        return Id;
                    case "content":
                        return Content;
                    case "defaulttitle":
                        return DefaultTitle;
                    case "defaultsummary":
                        return DefaultSummary;
                    case "defaultimageurl":
                        return DefaultImageUrl;
                    default:
                        break;
                }
            }
            return String.Empty;
        }
    }
}
