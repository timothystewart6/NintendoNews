using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppStudio.Controls
{
    public sealed partial class WebControl : UserControl
    {
        public WebControl()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
            this.SizeChanged += OnSizeChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            webView.DefaultBackgroundColor = Windows.UI.Colors.Transparent;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            webView.Width = e.NewSize.Width;
            webView.Height = e.NewSize.Height;
        }

        public string Html
        {
            get { return GetValue(HtmlProperty) as String; }
            set { SetValue(HtmlProperty, value); }
        }

        private void NavigateToString(string text)
        {
            webView.NavigateToString(text);
        }

        public static readonly DependencyProperty HtmlProperty = DependencyProperty.Register("Html", typeof(string), typeof(WebControl), new PropertyMetadata("", OnHtmlChanged));

        static private void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string html = String.Format(HTML_PATTERN, (e.NewValue as String) ?? String.Empty);
            var webControl = d as WebControl;
            webControl.NavigateToString(html);
        }

        const string HTML_PATTERN = "<!DOCTYPE html><html><head><style>body {{color: #f0f0f0; font-family: Segoe UI; font-size: 1.2em; }}</style></head><body>{0}</body></html>";
    }
}
