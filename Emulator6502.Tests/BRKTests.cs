namespace Emulator6502.Tests
{
    [TestFixture]
    public class BRKTests
    {
        [Test]
        public void BRK_Program_Counter_Set_To_Address_At_Break_Vector_Address()
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0x00 }, 0x00);

            //Manually Write the Break Address
            processor.WriteMemoryValue(0xFFFE, 0xBC);
            processor.WriteMemoryValue(0xFFFF, 0xCD);

            processor.NextStep();

            Assert.That(processor.ProgramCounter, Is.EqualTo(0xCDBC));
        }

        [Test]
        public void BRK_Program_Counter_Stack_Correct()
        {
            var processor = new Processor();

            processor.LoadProgram(0xABCD, new byte[] { 0x00 }, 0xABCD);

            var stackLocation = processor.StackPointer;
            processor.NextStep();

            Assert.That(processor.ReadMemoryValue(stackLocation + 0x100), Is.EqualTo(0xAB));
            Assert.That(processor.ReadMemoryValue(stackLocation + 0x100 - 1), Is.EqualTo(0xCF));
        }

        [Test]
        public void BRK_Stack_Pointer_Correct()
        {
            var processor = new Processor();

            processor.LoadProgram(0xABCD, new byte[] { 0x00 }, 0xABCD);

            var stackLocation = processor.StackPointer;
            processor.NextStep();

            Assert.That(processor.StackPointer, Is.EqualTo(stackLocation - 3));
        }

        [TestCase(0x038, 0x31)] //SEC Carry Flag Test
        [TestCase(0x0F8, 0x38)] //SED Decimal Flag Test
        [TestCase(0x078, 0x34)] //SEI Interrupt Flag Test
        public void BRK_Stack_Set_Flag_Operations_Correctly(byte operation, byte expectedValue)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0x58, operation, 0x00 }, 0x00);

            var stackLocation = processor.StackPointer;
            processor.NextStep();
            processor.NextStep();
            processor.NextStep();

            //Accounting for the Offset in memory
            Assert.That(processor.ReadMemoryValue(stackLocation + 0x100 - 2), Is.EqualTo(expectedValue));
        }

        [TestCase(0x01, 0x80, 0xB0)] //Negative
        [TestCase(0x01, 0x7F, 0xF0)] //Overflow + Negative
        [TestCase(0x00, 0x00, 0x32)] //Zero
        public void BRK_Stack_Non_Set_Flag_Operations_Correctly(byte accumulatorValue, byte memoryValue,
            byte expectedValue)
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0x58, 0xA9, accumulatorValue, 0x69, memoryValue, 0x00 }, 0x00);

            var stackLocation = processor.StackPointer;
            processor.NextStep();
            processor.NextStep();
            processor.NextStep();
            processor.NextStep();

            //Accounting for the Offset in memory
            Assert.That(processor.ReadMemoryValue(stackLocation + 0x100 - 2), Is.EqualTo(expectedValue));
        }
    }
}
