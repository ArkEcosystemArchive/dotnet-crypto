using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkEcosystem.Crypto.Tests.Identity
{
    [TestClass]
    public class PrivateKeyTest
    {
        [TestMethod]
        public void Should_Be_True()
        {
            Assert.IsTrue(true);
        }

        // /** @test */
        // public function it_should_get_the_private_key_from_secret()
        // {
        //     $actual = TestClass::fromSecret('this is a top secret passphrase', Devnet::new());

        //     $this->assertInstanceOf(EcPublicKey::class, $actual);
        //     $this->assertInternalType('string', actual.getHex());
        //     $this->assertSame('d8839c2432bfd0a67ef10a804ba991eabba19f154a3d707917681d45822a5712', actual.getHex());
        // }
    }
}
