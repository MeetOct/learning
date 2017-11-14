using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Learning.Zookeeper.Models
{
    public class ApolloConfig
    {
        public IDictionary<string, string> Configurations { get; set; }

        public string ReleaseKey { get; set; }

        public override string ToString()
        {
            return $"configurations={string.Join(";",Configurations.Select(c=>$"{c.Key}:{c.Value}" ))};releaseKey={ReleaseKey}";
        }
    }
}
