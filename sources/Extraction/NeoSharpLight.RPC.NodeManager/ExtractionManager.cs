using System;
using NeoSharp.Core;
using NeoSharp.Core.Extensions;

namespace NeoSharpLight.RPC.NodeManager
{
    public class ExtractionManager : IExtractionManager
    {
        private readonly INeoSharpContext neoSharpContext;

        public ExtractionManager(INeoSharpContext neoSharpContext)
        {
            this.neoSharpContext = neoSharpContext;

            var extractionConfiguraction = neoSharpContext.ApplicationConfiguration.LoadConfiguration<ExtractionConfiguration>();
        }

        public void StartExtraction()
        {
            throw new NotImplementedException();
        }
    }
}
