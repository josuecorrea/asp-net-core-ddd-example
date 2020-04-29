using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetCore.Example.UnitTest
{
    [TestClass]
    public class InformationRepositoryTest
    {
        //private readonly Mock<IInformationRepository> _informationRepository;
        
        [TestMethod]
        public void Get_CompanyInformation_Success()
        {
            //informationRepository = new Mock<IInformationRepository>();

            //var _mockConfSection = new Mock<IConfigurationSection>();
            //_mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "default")]).Returns("mock value");

            //var mockConfiguration = new Mock<IConfiguration>();
            //mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(_mockConfSection.Object);

            //var informationRepository = new InformationRepository(mockConfiguration.Object);

            //var result = informationRepository.GetCompanyInformationByCnpj("21450834000179").GetAwaiter().GetResult();

            //result.Should().NotBeNull();
        }
    }
}
