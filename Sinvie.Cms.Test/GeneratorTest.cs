using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Sinvie.Cms.IRepository;
using Sinvie.Cms.Repository.SqlServer;
using Sinvie.Cms.Core.Models;
using Sinvie.Cms.Core.CodeGenerator;
using Sinvie.Cms.Core.Options;
using Sinvie.Cms.Core.Repository;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Sinvie.Cms.Test {
    public class GeneratorTest {
        [Fact]
        public void GeneratorModelForSqlServer() {
            var serviceProvider = Common.BuildServiceForSqlServer();
            var codeGenerator = serviceProvider.GetRequiredService<CodeGenerator>();
            codeGenerator.GenerateTemplateCodesFromDatabase(true);
            Assert.Equal("SQLServer", DatabaseType.SqlServer.ToString(), ignoreCase: true);
                       

        }
    }

}
