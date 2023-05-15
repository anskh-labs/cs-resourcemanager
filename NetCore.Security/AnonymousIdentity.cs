using System;

namespace NetCore.Security
{
    public class AnonymousIdentity : UserIdentity
    {
        public AnonymousIdentity() : base(0, string.Empty, Array.Empty<string>(), Array.Empty<string>()) { }
    }
}
