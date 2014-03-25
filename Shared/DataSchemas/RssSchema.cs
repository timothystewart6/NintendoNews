using System;

namespace AppStudio.Data
{
    /// <summary>
    /// Implementation of the RssSchema.
    /// </summary>
    public class RssSchema : BindableSchemaBase
    {
        private string _title;
        private string _author;
        private DateTime _publishDate;
        private string _summary;
        private string _content;
        private string _imageUrl;
        private string _extraImageUrl;
        private string _mediaUrl;
        private string _feedUrl;

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

        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }

        public string ExtraImageUrl
        {
            get { return _extraImageUrl; }
            set { SetProperty(ref _extraImageUrl, value); }
        }

        public string MediaUrl
        {
            get { return _mediaUrl; }
            set { SetProperty(ref _mediaUrl, value); }
        }

        public string FeedUrl
        {
            get { return _feedUrl; }
            set { SetProperty(ref _feedUrl, value); }
        }

        public string Author
        {
            get { return _author; }
            set { SetProperty(ref _author, value); }
        }

        public DateTime PublishDate
        {
            get { return _publishDate; }
            set { SetProperty(ref _publishDate, value); }
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
                    case "content":
                        return Content;
                    case "imageurl":
                        return ImageUrl;
                    case "extraimageurl":
                        return ExtraImageUrl;
                    case "mediaurl":
                        return MediaUrl;
                    case "feedurl":
                        return FeedUrl;
                    case "author":
                        return Author;
                    case "publishdate":
                        return PublishDate.ToString();
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
