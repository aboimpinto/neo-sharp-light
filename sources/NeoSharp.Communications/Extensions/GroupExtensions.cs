using System.Text.RegularExpressions;

namespace NeoSharp.Communications.Extensions
{
    public static class GroupExtensions
    {
        public static string MatchGroupValue(this Group group)
        {
            return group.Success ? group.Value : null;
        }
    }
}