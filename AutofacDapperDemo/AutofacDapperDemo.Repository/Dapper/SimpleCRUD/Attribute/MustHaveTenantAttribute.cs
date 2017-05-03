using System;

namespace Dapper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MustHaveTenantAttribute : Attribute
    {
    }
}
