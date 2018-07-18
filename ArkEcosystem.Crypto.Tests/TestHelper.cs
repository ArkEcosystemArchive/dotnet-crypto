using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArkEcosystem.Crypto.Tests
{
    public static class TestHelper
    {
        const string FIXTURES_PATH = "../../../Fixtures/";

        public static Dictionary<string, dynamic> ReadTransactionFixture(string transactionType, string fixtureName)
        {
            var path = Path.Combine("Transactions", transactionType, fixtureName);
            return ReadFixture(path);
        }

        public static Dictionary<string, dynamic> ReadFixture(string name)
        {
            var path = Path.Combine(FIXTURES_PATH, name + ".json");
            var fixture = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(fixture);
        }
    }
}
