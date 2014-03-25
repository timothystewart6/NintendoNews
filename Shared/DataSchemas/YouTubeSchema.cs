using System;

namespace AppStudio.Data
{
    /// <summary>
    /// Implementation of the YouTube schema.
    /// </summary>
    public class YouTubeSchema : BindableSchemaBase
    {
        private const string YoutubeWatchBaseUrl = "http://www.youtube.com/watch?v=";

        private string _title;
        private string _videoUrl;
        private string _ImageUrl;
        private string _videoId;
        private string _summary;

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

        public string VideoUrl
        {
            get { return _videoUrl; }
            set { SetProperty(ref _videoUrl, value); }
        }

        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { SetProperty(ref _ImageUrl, value); }
        }

        public string VideoId
        {
            get
            {
                if (!string.IsNullOrEmpty(VideoUrl))
                {
                    var parsed = VideoUrl.Split('/');
                    _videoId = parsed[parsed.Length - 1];
                }
                return _videoId;
            }
            set { SetProperty(ref _videoId, value); }
        }

        public string ExternalUrl
        {
            get { return YoutubeWatchBaseUrl + VideoId; }
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
                    case "videourl":
                        return VideoUrl;
                    case "imageurl":
                        return ImageUrl;
                    case "videoid":
                        return VideoId;
                    case "externalurl":
                        return ExternalUrl;
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
