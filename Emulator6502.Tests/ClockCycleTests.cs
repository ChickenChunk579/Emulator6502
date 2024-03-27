using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Tests
{
    [TestFixture]
    public class ClockCycleTests
    {
        [TestCase(0xFA, OpcodeEnum.SEC, OpcodeEnum.BCS, 0x0A, 3)]
        [TestCase(0x00, OpcodeEnum.SEC, OpcodeEnum.BCS, 0x01, 2)]
        [TestCase(0x80, OpcodeEnum.SEC, OpcodeEnum.BCS, 0x7E, 3)]
        public void TestClockCyclesIncreasesInBranch(byte programOffset, OpcodeEnum setFlagOperation, OpcodeEnum branchOperation, byte offset, int expectedCycles)
        {
            Processor processor = new Processor();

            processor.LoadProgram(programOffset, [(byte)setFlagOperation, (byte)branchOperation, offset], programOffset);
            processor.NextStep();
            processor.Cycle();

            Assert.That(processor.Cycles, Is.EqualTo(expectedCycles));
        }

        [TestCase(0x00, OpcodeEnum.LDA_ABSX, 0x01, 0xFF, 4)] // No page crossing
        [TestCase(0x00, OpcodeEnum.LDA_ABSX, 0x01, 0x00, 5)] // Page crossing
        public void TestAbsoluteXPageBoundaryCycle(byte programOffset, OpcodeEnum operation, byte lowAddress, byte highAddress, int expectedCycles)
        {
            Processor processor = new Processor();

            // Set up the processor state
            processor.XRegister = 0x05; // X register value to cause page crossing
            processor.LoadProgram(programOffset, [(byte)operation, lowAddress, highAddress], programOffset);

            // Execute the instruction
            processor.NextStep();
            processor.Cycle();

            // Verify the number of cycles
            Assert.That(processor.Cycles, Is.EqualTo(expectedCycles));
        }

    }
}
