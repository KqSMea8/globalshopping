using System;
using System.Reflection;

namespace Baichuan.Api.Parser
{
    public class TopAttribute
    {
        public string ItemName { get; set; }
        public Type ItemType { get; set; }
        public string ListName { get; set; }
        public Type ListType { get; set; }
        public MethodInfo Method { get; set; }
    }
}
