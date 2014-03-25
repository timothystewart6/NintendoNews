using System;

namespace AppStudio.Data
{
    /// <summary>
    /// Implementation of the Flickr schema.
    /// </summary>
    public class FlickrSchema : BindableSchemaBase
    {
        private string _title;
        private string _summary;
        private string _imageUrl;
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

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
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
            get { return ImageUrl; }
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
                    case "imageurl":
                        return ImageUrl;
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
