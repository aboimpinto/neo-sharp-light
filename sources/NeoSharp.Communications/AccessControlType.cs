namespace NeoSharp.Communications
{
    public enum AccessControlType
    {
        None,       // no access control

        Whitelist,  // if match deny
        
        Blacklist,  // if match allow
    }
}