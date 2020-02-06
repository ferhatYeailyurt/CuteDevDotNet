using System;

namespace CuteDev.Api
{
    public class PermissionDetailAttribute : Attribute
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}