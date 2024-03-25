using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Tests
{
    [TestFixture]
    public class CompareTests
    {
        [TestCase(0x00, 0x00, true)]
        [TestCase(0xFF, 0x00, false)]
        [TestCase(0x00, 0xFF, false)]
        [TestCase(0xFF, 0xFF, true)]
        public void CMP_Zero_Flag_Set_When_Values_Match(byte accumulatorValue, byte memoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA9, accumulatorValue, 0xC9, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.ZeroFlag, Is.EqualTo(expectedResult));
        }


        [TestCase(0x00, 0x00, true)]
        [TestCase(0xFF, 0x00, true)]
        [TestCase(0x00, 0xFF, false)]
        [TestCase(0x00, 0x01, false)]
        [TestCase(0xFF, 0xFF, true)]
        public void CMP_Carry_Flag_Set_When_Accumulator_Is_Greater_Than_Or_Equal(byte accumulatorValue,
            byte memoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA9, accumulatorValue, 0xC9, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.CarryFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0xFE, 0xFF, true)]
        [TestCase(0x81, 0x1, true)]
        [TestCase(0x81, 0x2, false)]
        [TestCase(0x79, 0x1, false)]
        [TestCase(0x00, 0x1, true)]
        public void CMP_Negative_Flag_Set_When_Result_Is_Negative(byte accumulatorValue, byte memoryValue,
            bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA9, accumulatorValue, 0xC9, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.NegativeFlag, Is.EqualTo(expectedResult));
        }


        [TestCase(0x00, 0x00, true)]
        [TestCase(0xFF, 0x00, false)]
        [TestCase(0x00, 0xFF, false)]
        [TestCase(0xFF, 0xFF, true)]
        public void CPX_Zero_Flag_Set_When_Values_Match(byte xValue, byte memoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA2, xValue, 0xE0, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.ZeroFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x00, 0x00, true)]
        [TestCase(0xFF, 0x00, true)]
        [TestCase(0x00, 0xFF, false)]
        [TestCase(0x00, 0x01, false)]
        [TestCase(0xFF, 0xFF, true)]
        public void CPX_Carry_Flag_Set_When_Accumulator_Is_Greater_Than_Or_Equal(byte xValue, byte memoryValue,
            bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA2, xValue, 0xE0, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.CarryFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0xFE, 0xFF, true)]
        [TestCase(0x81, 0x1, true)]
        [TestCase(0x81, 0x2, false)]
        [TestCase(0x79, 0x1, false)]
        [TestCase(0x00, 0x1, true)]
        public void CPX_Negative_Flag_Set_When_Result_Is_Negative(byte xValue, byte memoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA2, xValue, 0xE0, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.NegativeFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x00, 0x00, true)]
        [TestCase(0xFF, 0x00, false)]
        [TestCase(0x00, 0xFF, false)]
        [TestCase(0xFF, 0xFF, true)]
        public void CPY_Zero_Flag_Set_When_Values_Match(byte xValue, byte memoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA0, xValue, 0xC0, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.ZeroFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0x00, 0x00, true)]
        [TestCase(0xFF, 0x00, true)]
        [TestCase(0x00, 0xFF, false)]
        [TestCase(0x00, 0x01, false)]
        [TestCase(0xFF, 0xFF, true)]
        public void CPY_Carry_Flag_Set_When_Accumulator_Is_Greater_Than_Or_Equal(byte xValue, byte memoryValue,
            bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA0, xValue, 0xC0, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.CarryFlag, Is.EqualTo(expectedResult));
        }

        [TestCase(0xFE, 0xFF, true)]
        [TestCase(0x81, 0x1, true)]
        [TestCase(0x81, 0x2, false)]
        [TestCase(0x79, 0x1, false)]
        [TestCase(0x00, 0x1, true)]
        public void CPY_Negative_Flag_Set_When_Result_Is_Negative(byte xValue, byte memoryValue, bool expectedResult)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xA0, xValue, 0xC0, memoryValue }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.NegativeFlag, Is.EqualTo(expectedResult));
        }
    }
}
