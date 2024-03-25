
namespace Emulator6502.Tests
{
    [TestFixture]
    public class BITTests
    {
        [TestCase(0x24, 0x7f, 0x7F, false)] // BIT Zero Page
        [TestCase(0x24, 0x80, 0x7F, false)] // BIT Zero Page
        [TestCase(0x24, 0x7F, 0x80, true)] // BIT Zero Page
        [TestCase(0x24, 0x80, 0xFF, true)] // BIT Zero Page
        [TestCase(0x24, 0xFF, 0x80, true)] // BIT Zero Page
        [TestCase(0x2C, 0x7F, 0x7F, false)] // BIT Absolute
        [TestCase(0x2C, 0x80, 0x7F, false)] // BIT Absolute
        [TestCase(0x2C, 0x7F, 0x80, true)] // BIT Absolute
        [TestCase(0x2C, 0x80, 0xFF, true)] // BIT Absolute
        [TestCase(0x2C, 0xFF, 0x80, true)] // BIT Absolute
        public void BIT_Negative_Set_When_Comparison_Is_Negative_Number(byte operation, byte accumulatorValue,
            byte valueToTest, bool expectedResult)
        {
            var processor = new Processor();
            Assert.That(processor.ProgramCounter, Is.EqualTo(0));

            processor.LoadProgram(0x00, new byte[] { 0xA9, accumulatorValue, operation, 0x06, 0x00, 0x00, valueToTest },
                0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.NegativeFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x24, 0x3F, 0x3F, false)] // BIT Zero Page
        [TestCase(0x24, 0x3F, 0x40, true)] // BIT Zero Page
        [TestCase(0x24, 0x40, 0x3F, false)] // BIT Zero Page
        [TestCase(0x24, 0x40, 0x7F, true)] // BIT Zero Page
        [TestCase(0x24, 0x7F, 0x40, true)] // BIT Zero Page
        [TestCase(0x24, 0x7F, 0x80, false)] // BIT Zero Page
        [TestCase(0x24, 0x80, 0x7F, true)] // BIT Zero Page
        [TestCase(0x24, 0xC0, 0xDF, true)] // BIT Zero Page
        [TestCase(0x24, 0xDF, 0xC0, true)] // BIT Zero Page
        [TestCase(0x24, 0x3F, 0x3F, false)] // BIT Zero Page
        [TestCase(0x24, 0xC0, 0xFF, true)] // BIT Zero Page
        [TestCase(0x24, 0xFF, 0xC0, true)] // BIT Zero Page
        [TestCase(0x24, 0x40, 0xFF, true)] // BIT Zero Page
        [TestCase(0x24, 0xFF, 0x40, true)] // BIT Zero Page
        [TestCase(0x24, 0xC0, 0x7F, true)] // BIT Zero Page
        [TestCase(0x24, 0x7F, 0xC0, true)] // BIT Zero Page
        [TestCase(0x2C, 0x3F, 0x3F, false)] // BIT Absolute
        [TestCase(0x2C, 0x3F, 0x40, true)] // BIT Absolute
        [TestCase(0x2C, 0x40, 0x3F, false)] // BIT Absolute
        [TestCase(0x2C, 0x40, 0x7F, true)] // BIT Absolute
        [TestCase(0x2C, 0x7F, 0x40, true)] // BIT Absolute
        [TestCase(0x2C, 0x7F, 0x80, false)] // BIT Absolute
        [TestCase(0x2C, 0x80, 0x7F, true)] // BIT Absolute
        [TestCase(0x2C, 0xC0, 0xDF, true)] // BIT Absolute
        [TestCase(0x2C, 0xDF, 0xC0, true)] // BIT Absolute
        [TestCase(0x2C, 0x3F, 0x3F, false)] // BIT Absolute
        [TestCase(0x2C, 0xC0, 0xFF, true)] // BIT Absolute
        [TestCase(0x2C, 0xFF, 0xC0, true)] // BIT Absolute
        [TestCase(0x2C, 0x40, 0xFF, true)] // BIT Absolute
        [TestCase(0x2C, 0xFF, 0x40, true)] // BIT Absolute
        [TestCase(0x2C, 0xC0, 0x7F, true)] // BIT Absolute
        [TestCase(0x2C, 0x7F, 0xC0, true)] // BIT Absolute
        public void BIT_Overflow_Set_By_Bit_Six(byte operation, byte accumulatorValue, byte valueToTest,
            bool expectedResult)
        {
            var processor = new Processor();
            Assert.That(processor.ProgramCounter, Is.EqualTo(0));

            processor.LoadProgram(0x00, new byte[] { 0xA9, accumulatorValue, operation, 0x06, 0x00, 0x00, valueToTest },
                0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.OverflowFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x24, 0x00, 0x00, true)] // BIT Zero Page
        [TestCase(0x24, 0xFF, 0xFF, false)] // BIT Zero Page
        [TestCase(0x24, 0xAA, 0x55, true)] // BIT Zero Page
        [TestCase(0x24, 0x55, 0xAA, true)] // BIT Zero Page
        [TestCase(0x2C, 0x00, 0x00, true)] // BIT Absolute
        [TestCase(0x2C, 0xFF, 0xFF, false)] // BIT Absolute
        [TestCase(0x2C, 0xAA, 0x55, true)] // BIT Absolute
        [TestCase(0x2C, 0x55, 0xAA, true)] // BIT Absolute
        public void BIT_Zero_Set_When_Comparison_Is_Zero(byte operation, byte accumulatorValue, byte valueToTest,
            bool expectedResult)
        {
            var processor = new Processor();
            Assert.That(processor.ProgramCounter, Is.EqualTo(0));

            processor.LoadProgram(0x00, new byte[] { 0xA9, accumulatorValue, operation, 0x06, 0x00, 0x00, valueToTest },
                0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.ZeroFlag, Is.EqualTo(expectedResult));
        }

    }
}
