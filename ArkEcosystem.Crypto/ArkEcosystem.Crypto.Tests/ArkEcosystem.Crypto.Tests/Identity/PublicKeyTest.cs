using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkEcosystem.Crypto.Tests.Identity
{
    [TestClass]
    public class PublicKeyTest
    {
        [TestMethod]
        public void Should_Be_True()
        {
            Assert.IsTrue(true);
        }

        // /** @test */
        // public function it_should_get_the_public_key_from_secret()
        // {
        //     $actual = TestClass::fromSecret('this is a top secret passphrase', Devnet::new());

        //     $this->assertInstanceOf(EcPublicKey::class, $actual);
        //     $this->assertInternalType('string', actual.getHex());
        //     $this->assertSame('034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192', actual.getHex());
        // }
    }
}
