using WireMock.Server;
using WireMock.Settings;
using NUnit.Framework;
using Reqnroll;

[Binding]
public class WiremockBaseTest
{
    protected static WireMockServer server;

    [BeforeScenario]
    public void Setup()
    {
        if (server != null) return;
        server = WireMockServer.Start(new WireMockServerSettings
        {
            Urls = 
            [
                "http://localhost:9091/" 
            ]
        });
    }

    [AfterScenario]
    public void TearDown()
    {
        server.Stop();
        server.Dispose();
    }
}

