namespace Emulator6502.Tests
{
    [TestFixture]
    public class EORTests
    {
        [TestCase(0x00, 0x00, 0x00)]
        [TestCase(0xFF, 0x00, 0xFF)]
        [TestCase(0x00, 0xFF, 0xFF)]
        [TestCase(0x55, 0xAA, 0xFF)]
        [TestCase(0xFF, 0xFF, 0x00)]
        public void EOR_Accumulator_Correct(byte accumulatorValue, byte memoryValue, byte expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA9, accumulatorValue, 0x49, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.Accumulator, Is.EqualTo(expectedResult));
        }

        [TestCase(0xFF, 0xFF, false)]
        [TestCase(0x80, 0x7F, true)]
        [TestCase(0x40, 0x3F, false)]
        [TestCase(0xFF, 0x7F, true)]
        public void EOR_Negative_Flag_Correct(byte accumulatorValue, byte memoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA9, accumulatorValue, 0x49, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.NegativeFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0xFF, 0xFF, true)]
        [TestCase(0x80, 0x7F, false)]
        public void EOR_Zero_Flag_Correct(byte accumulatorValue, byte memoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA9, accumulatorValue, 0x49, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.ZeroFlag, Is.EqualTo(expectedResult));
        }
    }
}
