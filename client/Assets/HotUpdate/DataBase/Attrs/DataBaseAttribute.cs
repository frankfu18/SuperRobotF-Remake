using System;

namespace db.core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class DataBaseAttribute : Attribute
    {
        public Type SubType;

        public DataBaseAttribute(Type type = null)
        {
            SubType = type;
        }
    }
}