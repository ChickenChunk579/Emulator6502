using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Tests
{
    [TestFixture]
    public class DecrementTests
    {
        [TestCase(0x00, 0xFF)]
        [TestCase(0xFF, 0xFE)]
        public void DEC_Memory_Has_Correct_Value(byte initialMemoryValue, byte expectedMemoryValue)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xC6, 0x03, 0x00, initialMemoryValue }, 0x00);
            processor.NextStep();

            Assert.That(processor.ReadMemoryValue(0x03), Is.EqualTo(expectedMemoryValue));
        }

        [TestCase(0x00, false)]
        [TestCase(0x01, true)]
        [TestCase(0x02, false)]
        public void DEC_Zero_Has_Correct_Value(byte initialMemoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xC6, 0x03, 0x00, initialMemoryValue }, 0x00);
            processor.NextStep();

            Assert.That(processor.ZeroFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x80, false)]
        [TestCase(0x81, true)]
        [TestCase(0x00, true)]
        public void DEC_Negative_Has_Correct_Value(byte initialMemoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xC6, 0x03, 0x00, initialMemoryValue }, 0x00);
            processor.NextStep();

            Assert.That(processor.NegativeFlag, Is.EqualTo(expectedResult));
        }


        [TestCase(0x00, 0xFF)]
        [TestCase(0xFF, 0xFE)]
        public void DEX_XRegister_Has_Correct_Value(byte initialXRegisterValue, byte expectedMemoryValue)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA2, initialXRegisterValue, 0xCA }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.XRegister, Is.EqualTo(expectedMemoryValue));
        }

        [TestCase(0x00, false)]
        [TestCase(0x01, true)]
        [TestCase(0x02, false)]
        public void DEX_Zero_Has_Correct_Value(byte initialXRegisterValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA2, initialXRegisterValue, 0xCA }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.ZeroFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x80, false)]
        [TestCase(0x81, true)]
        [TestCase(0x00, true)]
        public void DEX_Negative_Has_Correct_Value(byte initialXRegisterValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA2, initialXRegisterValue, 0xCA }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.NegativeFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x00, 0xFF)]
        [TestCase(0xFF, 0xFE)]
        public void DEY_YRegister_Has_Correct_Value(byte initialYRegisterValue, byte expectedMemoryValue)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA0, initialYRegisterValue, 0x88 }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.YRegister, Is.EqualTo(expectedMemoryValue));
        }

        [TestCase(0x00, false)]
        [TestCase(0x01, true)]
        [TestCase(0x02, false)]
        public void DEY_Zero_Has_Correct_Value(byte initialYRegisterValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA0, initialYRegisterValue, 0x88 }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.ZeroFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x80, false)]
        [TestCase(0x81, true)]
        [TestCase(0x00, true)]
        public void DEY_Negative_Has_Correct_Value(byte initialYRegisterValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA0, initialYRegisterValue, 0x88 }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.NegativeFlag, Is.EqualTo(expectedResult));
        }
    }
}
