namespace NeoSharpLight.RPC.BlockchainExtraction.Storage
{
    public interface IStorageAccess
    {
        void SetParameter(string key, string value);

        string GetParameter(string key);
    }
}