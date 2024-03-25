namespace Emulator6502.Tests
{
    [TestFixture]
    public class FlagTests
    {
        [Test]
        public void CheckThatFlagsAreSetToExpectedValues()
        {
            Assert.That((byte)Flags.CarryBit, Is.EqualTo(0x1));
            Assert.That((byte)Flags.Zero, Is.EqualTo(0x2));
            Assert.That((byte)Flags.DisableInterrupts, Is.EqualTo(0x4));
            Assert.That((byte)Flags.Decimal, Is.EqualTo(0x8));
            Assert.That((byte)Flags.Break, Is.EqualTo(0x10));
            Assert.That((byte)Flags.Unused, Is.EqualTo(0x20));
            Assert.That((byte)Flags.Overflow, Is.EqualTo(0x40));
            Assert.That((byte)Flags.Negative, Is.EqualTo(0x80));
        }

        [TestCase(Flags.CarryBit)]
        [TestCase(Flags.Zero)]
        [TestCase(Flags.DisableInterrupts)]
        [TestCase(Flags.Decimal)]
        [TestCase(Flags.Break)]
        [TestCase(Flags.Unused)]
        [TestCase(Flags.Overflow)]
        [TestCase(Flags.Negative)]
        public void WhenSetFlagThenGetFlagReturnsTrue(Flags flagToTest)
        {
            byte statusByte = 0;

            statusByte.SetFlag(flagToTest, true);

            Assert.That(statusByte, Is.EqualTo((byte)flagToTest));
            Assert.IsTrue(statusByte.GetFlag(flagToTest));
        }
    }
}
