using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Webhook.Helpers
{
    public class ConfigSection : ConfigurationSection
    {
        public static ConfigSection Webhook
        {
            get { return (ConfigSection)ConfigurationManager.GetSection("webhook"); }
        }

        public Dictionary<string, UrlConfigurationElement> Data
        {
            get
            {
                return Hooks.OfType<UrlConfigurationElement>().ToDictionary(x => x.Name, x => x);
            }
        }

        [ConfigurationProperty("hooks", IsDefaultCollection = true)]
        public UrlConfigurationElementCollection Hooks
        {
            get { return (UrlConfigurationElementCollection)this["hooks"]; }
            set { this["hooks"] = value; }
        }

        [ConfigurationCollection(typeof(UrlConfigurationElement))]
        public class UrlConfigurationElementCollection : ConfigurationElementCollection
        {
            protected override ConfigurationElement CreateNewElement()
            {
                return new UrlConfigurationElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((UrlConfigurationElement)element).Name;
            }

            [ConfigurationProperty("enable", IsKey = true, IsRequired = false)]
            public bool Enable
            {
                get { return (bool)this["enable"]; }
                set { this["enable"] = value; }
            }
        }

        public class UrlConfigurationElement : ConfigurationElement
        {
            [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
            public string Name
            {
                get { return (string)this["name"]; }
                set { this["name"] = value; }
            }

            [ConfigurationProperty("url", IsRequired = true)]
            public string Url
            {
                get { return (string)this["url"]; }
                set { this["url"] = value; }
            }

            [ConfigurationProperty("method", IsRequired = true)]
            public string Method
            {
                get { return (string)this["method"]; }
                set { this["method"] = value; }
            }
        }
    }
}