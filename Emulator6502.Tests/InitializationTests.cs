namespace Emulator6502.Tests
{
    public class InitializationTests
    {
        [Test]
        public void StatusFlagsInitializedCorrectly()
        {
            Processor processor = new Processor();

            Assert.That(processor.CarryFlag, Is.EqualTo(false));
            Assert.That(processor.ZeroFlag, Is.EqualTo(false));
            Assert.That(processor.DisableInterruptsFlag, Is.EqualTo(false));
            Assert.That(processor.DecimalFlag, Is.EqualTo(false));
            Assert.That(processor.OverflowFlag, Is.EqualTo(false));
            Assert.That(processor.NegativeFlag, Is.EqualTo(false));
        }

        [Test]
        public void RegistersInitializedCorrectly()
        {
            Processor processor = new Processor();

            Assert.That(processor.Accumulator, Is.EqualTo(0));
            Assert.That(processor.XRegister, Is.EqualTo(0));
            Assert.That(processor.YRegister, Is.EqualTo(0));
            Assert.That(processor.CurrentOpCode, Is.EqualTo(null));
            Assert.That(processor.ProgramCounter, Is.EqualTo(0));
        }

        [Test]
        public void ThrowsExceptionWhenOpCodeIsInvalid()
        {
            Processor processor = new Processor();

            processor.LoadProgram(0x00, [0xFF], 0x00);

            Assert.Throws<NotSupportedException>(processor.NextStep);
        }

        [Test]
        public void StackPointerInitializesToDefaultValueAfterReset()
        {
            var processor = new Processor();
            processor.Reset();

            Assert.That(processor.StackPointer, Is.EqualTo(0xFD));
        }
    }
}