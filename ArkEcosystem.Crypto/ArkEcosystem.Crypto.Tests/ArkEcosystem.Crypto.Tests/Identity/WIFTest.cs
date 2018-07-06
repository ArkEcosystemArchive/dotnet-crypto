using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkEcosystem.Crypto.Tests.Identity
{
    [TestClass]
    public class WIFTest
    {
        [TestMethod]
        public void Should_Be_True()
        {
            Assert.IsTrue(true);
        }

        // /** @test */
        // public function it_should_get_the_wif_from_secret()
        // {
        //     $actual = TestClass::fromSecret('this is a top secret passphrase', Devnet::new());

        //     $this->assertInternalType('string', $actual);
        //     $this->assertSame('SGq4xLgZKCGxs7bjmwnBrWcT4C1ADFEermj846KC97FSv1WFD1dA', $actual);
        // }
    }
}
