using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkEcosystem.Crypto.Tests.Identity
{
    [TestClass]
    public class AddressTest
    {
        [TestMethod]
        public void Should_Be_True()
        {
            Assert.IsTrue(true);
        }

        // /** @test */
        // public function it_should_get_the_address_from_public_key()
        // {
        //     $actual = TestClass::fromPublicKey('034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192', Devnet::new());

        //     $this->assertInternalType('string', $actual);
        //     $this->assertSame('D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib', $actual);
        // }

        // /** @test */
        // public function it_should_get_the_address_from_secret()
        // {
        //     $actual = TestClass::fromSecret('this is a top secret passphrase', Devnet::new());

        //     $this->assertInternalType('string', $actual);
        //     $this->assertSame('D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib', $actual);
        // }

        // /** @test */
        // public function it_should_get_the_address_from_private_key()
        // {
        //     $privateKey = PrivateKey::fromSecret('this is a top secret passphrase');

        //     $actual = TestClass::fromPrivateKey($privateKey, Devnet::new());

        //     $this->assertInternalType('string', $actual);
        //     $this->assertSame('D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib', $actual);
        // }

        // /** @test */
        // public function it_should_validate_the_address()
        // {
        //     $actual = TestClass::validate('D61mfSggzbvQgTUe6JhYKH2doHaqJ3Dyib', Devnet::new());

        //     $this->assertInternalType('boolean', $actual);
        //     $this->assertTrue($actual);
        // }
    }
}
