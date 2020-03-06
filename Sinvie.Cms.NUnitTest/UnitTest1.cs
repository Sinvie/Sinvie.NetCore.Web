using NUnit.Framework;
using Sinvie.Cms.Core.CodeGenerator;
using Sinvie.Cms.Core.Models;


using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Sinvie.Cms.NUnitTest {
    public class Tests {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void BuildServiceForSqlServer_Test() {
            var serviceProvider = Common.BuildServiceForSqlServer();
            var codeGenerator = serviceProvider.GetRequiredService<CodeGenerator>();
            codeGenerator.GenerateTemplateCodesFromDatabase(true);
            Assert.AreEqual("SqlServer", DatabaseType.SqlServer.ToString());
            Assert.Pass();
        }
    }
}