using System;

namespace Learning.Zookeeper.Exceptions
{
    public class ApolloConfigException : Exception
    {
        public ApolloConfigException(string message) : base(message) { }
        public ApolloConfigException(string message, Exception ex) : base(message, ex) { }
    }
}
