using Hanssens.Net;

namespace NeoSharpLight.RPC.BlockchainExtraction.Storage
{
    public class StorageAccess : IStorageAccess
    {
        private LocalStorage _localStorage;

        public StorageAccess()
        {
            this._localStorage = new LocalStorage();
        }

        public string GetParameter(string key)
        {
            return this._localStorage.Get(key).ToString();
        }

        public void SetParameter(string key, string value)
        {
            this._localStorage.Store(key, value);
            this._localStorage.Persist();
        }
    }
}