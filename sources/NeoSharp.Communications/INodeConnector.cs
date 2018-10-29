namespace NeoSharp.Communications
{
    public interface INodeConnector
    {
        /// <summary>
        /// Connect the node to the network defined in the appsettings.json file.
        /// </summary>
         void Connect();
    }
}