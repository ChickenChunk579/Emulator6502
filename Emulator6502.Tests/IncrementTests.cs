namespace Emulator6502.Tests
{
    [TestFixture]
    public class IncrementTests
    {
        [TestCase(0x00, 0x01)]
        [TestCase(0xFF, 0x00)]
        public void INC_Memory_Has_Correct_Value(byte initialMemoryValue, byte expectedMemoryValue)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xE6, 0x03, 0x00, initialMemoryValue }, 0x00);
            processor.NextStep();

            Assert.That(processor.ReadMemoryValue(0x03), Is.EqualTo(expectedMemoryValue));
        }

        [TestCase(0x00, false)]
        [TestCase(0xFF, true)]
        [TestCase(0xFE, false)]
        public void INC_Zero_Has_Correct_Value(byte initialMemoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xE6, 0x03, 0x00, initialMemoryValue }, 0x00);
            processor.NextStep();

            Assert.That(processor.SR.ZeroFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x78, false)]
        [TestCase(0x80, true)]
        [TestCase(0x00, false)]
        public void INC_Negative_Has_Correct_Value(byte initialMemoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xE6, 0x02, initialMemoryValue }, 0x00);
            processor.NextStep();

            Assert.That(processor.SR.NegativeFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x00, 0x01)]
        [TestCase(0xFF, 0x00)]
        public void INX_XRegister_Has_Correct_Value(byte initialXRegister, byte expectedMemoryValue)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA2, initialXRegister, 0xE8 }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.XRegister, Is.EqualTo(expectedMemoryValue));
        }

        [TestCase(0x00, false)]
        [TestCase(0xFF, true)]
        [TestCase(0xFE, false)]
        public void INX_Zero_Has_Correct_Value(byte initialXRegister, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA2, initialXRegister, 0xE8 }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.ZeroFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x78, false)]
        [TestCase(0x80, true)]
        [TestCase(0x00, false)]
        public void INX_Negative_Has_Correct_Value(byte initialXRegister, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA2, initialXRegister, 0xE8 }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.NegativeFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x00, 0x01)]
        [TestCase(0xFF, 0x00)]
        public void INY_YRegister_Has_Correct_Value(byte initialYRegister, byte expectedMemoryValue)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA0, initialYRegister, 0xC8 }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.YRegister, Is.EqualTo(expectedMemoryValue));
        }

        [TestCase(0x00, false)]
        [TestCase(0xFF, true)]
        [TestCase(0xFE, false)]
        public void INY_Zero_Has_Correct_Value(byte initialYRegister, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA0, initialYRegister, 0xC8 }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.ZeroFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x78, false)]
        [TestCase(0x80, true)]
        [TestCase(0x00, false)]
        public void INY_Negative_Has_Correct_Value(byte initialYRegister, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA0, initialYRegister, 0xC8 }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.NegativeFlag, Is.EqualTo(expectedResult));
        }

    }
}
