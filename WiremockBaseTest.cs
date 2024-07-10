using WireMock.Server;
using WireMock.Settings;
using NUnit.Framework;

public class WiremockBaseTest
{
    protected static WireMockServer server;

    [OneTimeSetUp]
    public void Setup()
    {
        server = WireMockServer.Start(new WireMockServerSettings
        {
            Urls = 
            [
                "http://localhost:9091/" 
            ]
        });
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        server.Stop();
        server.Dispose();
    }
}

