using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AppStudio.Data
{
    public abstract class BindableSchemaBase : BindableBase
    {
        /// <summary>
        /// Identifier for this instance
        /// </summary>
        private string _id = null;
        public string Id
        {
            get { return String.IsNullOrEmpty(_id) ? Guid.NewGuid().ToString() : _id; }
            set { _id = value; }
        }

        abstract public string DefaultTitle { get; }
        abstract public string DefaultSummary { get; }
        abstract public string DefaultImageUrl { get; }

        abstract public string GetValue(string propertyName);

        virtual public string GetValues(params string[] propertyNames)
        {
            StringBuilder strb = new StringBuilder();
            foreach (var propertyName in propertyNames)
            {
                object value = GetValue(propertyName) ?? String.Empty;
                strb.AppendLine(value.ToString());
            }
            return strb.ToString();
        }
    }
}
