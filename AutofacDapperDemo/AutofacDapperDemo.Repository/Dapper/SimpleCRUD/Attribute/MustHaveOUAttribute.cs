using System;

namespace Dapper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MustHaveOUAttribute : Attribute
    {
    }
}
