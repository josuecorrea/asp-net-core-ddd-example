using EasyNetQ.Topology;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Example.Infra.Services
{
    public class ExchangeConfig : IExchange
    {
        private const string ExchangeName = "Teste_Exchange";
        private const string ExchangeType = "fanout";

        public ExchangeConfig()
        {
            Name = ExchangeName;
            Type = ExchangeType;
        }

        public string Name { get; private set; }

        public string Type { get; private set; }
    }
}
