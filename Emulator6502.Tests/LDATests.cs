namespace Emulator6502.Tests
{
    [TestFixture]
    public class LDATests
    {
        [Test]
        public void LDA_Accumulator_Has_Correct_Value()
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA9, 0x03 }, 0x00);
            processor.NextStep();

            Assert.That(processor.Accumulator, Is.EqualTo(0x03));
        }

        [TestCase(0x0, true)]
        [TestCase(0x3, false)]
        public void LDA_Zero_Set_Correctly(byte valueToLoad, bool expectedValue)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA9, valueToLoad }, 0x00);
            processor.NextStep();

            Assert.That(processor.ZeroFlag, Is.EqualTo(expectedValue));
        }

        [TestCase(0x00, false)]
        [TestCase(0x79, false)]
        [TestCase(0x80, true)]
        [TestCase(0xFF, true)]
        public void LDA_Negative_Set_Correctly(byte valueToLoad, bool expectedValue)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA9, valueToLoad }, 0x00);
            processor.NextStep();

            Assert.That(processor.NegativeFlag, Is.EqualTo(expectedValue));
        }

    }
}
