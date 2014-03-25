using System;

namespace AppStudio.Data
{
    /// <summary>
    /// Implementation of the Bing schema.
    /// </summary>
    public class BingSchema : BindableSchemaBase
    {
        private string _title;
        private string _summary;
        private string _link;
        private DateTime _published;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Summary
        {
            get { return _summary; }
            set { SetProperty(ref _summary, value); }
        }

        public string Link
        {
            get { return _link; }
            set { SetProperty(ref _link, value); }
        }

        public DateTime Published
        {
            get { return _published; }
            set { SetProperty(ref _published, value); }
        }

        public override string DefaultTitle
        {
            get { return Title; }
        }

        public override string DefaultSummary
        {
            get { return Summary; }
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
                    case "title":
                        return Title;
                    case "summary":
                        return Summary;
                    case "link":
                        return Link;
                    case "published":
                        return Published.ToString();
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
