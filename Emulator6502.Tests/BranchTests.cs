using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Tests
{
    [TestFixture]
    public class BranchTests
    {
        [TestCase(0, 1, 3)]
        [TestCase(0x80, 0x80, 2)]
        [TestCase(0, 0xFD, 0xFFFF)]
        [TestCase(0x7D, 0x80, 0xFFFF)]
        public void BCC_Program_Counter_Correct(int programCounterInitialValue, byte offset, int expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.ProgramCounter, Is.EqualTo(0));

            processor.LoadProgram(programCounterInitialValue, new byte[] { 0x90, offset }, programCounterInitialValue);
            processor.NextStep();

            Assert.That(processor.ProgramCounter, Is.EqualTo(expectedValue));
        }

        [TestCase(0, 1, 4)]
        [TestCase(0x80, 0x80, 3)]
        [TestCase(0, 0xFC, 0xFFFF)]
        [TestCase(0x7C, 0x80, 0xFFFF)]
        public void BCS_Program_Counter_Correct(int programCounterInitialValue, byte offset, int expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.ProgramCounter, Is.EqualTo(0));

            processor.LoadProgram(programCounterInitialValue, new byte[] { 0x38, 0xB0, offset },
                programCounterInitialValue);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.ProgramCounter, Is.EqualTo(expectedValue));
        }

        [TestCase(0, 1, 5)]
        [TestCase(0x80, 0x80, 4)]
        [TestCase(0, 0xFB, 0xFFFF)]
        [TestCase(0x7B, 0x80, 0xFFFF)]
        [TestCase(2, 0xFE, 4)]
        public void BEQ_Program_Counter_Correct(int programCounterInitialValue, byte offset, int expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.ProgramCounter, Is.EqualTo(0));

            processor.LoadProgram(programCounterInitialValue, new byte[] { 0xA9, 0x00, 0xF0, offset },
                programCounterInitialValue);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.ProgramCounter, Is.EqualTo(expectedValue));
        }

        [TestCase(0, 1, 5)]
        [TestCase(0x80, 0x80, 4)]
        [TestCase(0, 0xFB, 0xFFFF)]
        [TestCase(0x7B, 0x80, 0xFFFF)]
        public void BMI_Program_Counter_Correct(int programCounterInitialValue, byte offset, int expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.ProgramCounter, Is.EqualTo(0));

            processor.LoadProgram(programCounterInitialValue, new byte[] { 0xA9, 0x80, 0x30, offset },
                programCounterInitialValue);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.ProgramCounter, Is.EqualTo(expectedValue));
        }

        [TestCase(0, 1, 5)]
        [TestCase(0x80, 0x80, 4)]
        [TestCase(0, 0xFB, 0xFFFF)]
        [TestCase(0x7B, 0x80, 0xFFFF)]
        public void BNE_Program_Counter_Correct(int programCounterInitialValue, byte offset, int expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.ProgramCounter, Is.EqualTo(0));

            processor.LoadProgram(programCounterInitialValue, new byte[] { 0xA9, 0x01, 0xD0, offset },
                programCounterInitialValue);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.ProgramCounter, Is.EqualTo(expectedValue));
        }

        [TestCase(0, 1, 5)]
        [TestCase(0x80, 0x80, 4)]
        [TestCase(0, 0xFB, 0xFFFF)]
        [TestCase(0x7B, 0x80, 0xFFFF)]
        public void BPL_Program_Counter_Correct(int programCounterInitialValue, byte offset, int expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.ProgramCounter, Is.EqualTo(0));

            processor.LoadProgram(programCounterInitialValue, new byte[] { 0xA9, 0x79, 0x10, offset },
                programCounterInitialValue);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.ProgramCounter, Is.EqualTo(expectedValue));
        }
    }
}
