using System;

namespace db.core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class DatabaseRootAttribute: Attribute
    {
        
    }
}