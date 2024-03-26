using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Tests
{
    [TestFixture]
    public class JumpTests
    {
        [Test]
        public void JMP_Program_Counter_Set_Correctly_After_Jump()
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0x4C, 0x08, 0x00 }, 0x00);
            processor.NextStep();

            Assert.That(processor.ProgramCounter, Is.EqualTo(0x08));
        }

        [Test]
        public void JMP_Program_Counter_Set_Correctly_After_Indirect_Jump()
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0x6C, 0x03, 0x00, 0x08, 0x00 }, 0x00);
            processor.NextStep();

            Assert.That(processor.ProgramCounter, Is.EqualTo(0x08));
        }

        [Test]
        public void JMP_Indirect_Wraps_Correct_If_MSB_IS_FF()
        {
            var processor = new Processor();
            processor.WriteMemoryValue(0x01FE, 0x6C);
            processor.LoadProgram(0, new byte[] { 0x6C, 0xFF, 0x01, 0x08, 0x00 }, 0x00);

            processor.WriteMemoryValue(0x01FF, 0x03);
            processor.WriteMemoryValue(0x0100, 0x02);
            processor.NextStep();

            Assert.That(processor.ProgramCounter, Is.EqualTo(0x0203));
        }


        [Test]
        public void JSR_Stack_Loads_Correct_Value()
        {
            var processor = new Processor();

            processor.LoadProgram(0xBBAA, new byte[] { 0x20, 0xCC, 0xCC }, 0xBBAA);

            var stackLocation = processor.StackPointer;
            processor.NextStep();


            Assert.That(processor.ReadMemoryValue(stackLocation + 0x100), Is.EqualTo(0xBB));
            Assert.That(processor.ReadMemoryValue(stackLocation + 0x100 - 1), Is.EqualTo(0xAC));
        }

        [Test]
        public void JSR_Program_Counter_Correct()
        {
            var processor = new Processor();

            processor.LoadProgram(0xBBAA, new byte[] { 0x20, 0xCC, 0xCC }, 0xBBAA);
            processor.NextStep();


            Assert.That(processor.ProgramCounter, Is.EqualTo(0xCCCC));
        }


        [Test]
        public void JSR_Stack_Pointer_Correct()
        {
            var processor = new Processor();

            processor.LoadProgram(0xBBAA, new byte[] { 0x20, 0xCC, 0xCC }, 0xBBAA);

            var stackLocation = processor.StackPointer;
            processor.NextStep();


            Assert.That(processor.StackPointer, Is.EqualTo(stackLocation - 2));
        }
    }
}
