using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NeoSharp.Model
{
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionType : int
    {
        MinerTransaction = 0,
        IssueTransaction = 1,
        ClaimTransaction = 2,
        EnrollmentTransaction = 3,
        RegisterTransaction = 4,
        ContractTransaction = 5,
        StateTransaction = 6,
        PublishTransaction = 7,
        InvocationTransaction = 8
    }
}
