namespace Emulator6502.Tests
{
    [TestFixture]
    public class FlagUpdaterTests
    {
        [Test]
        public void CLC_Carry_Flag_Cleared_Correctly()
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0x18 }, 0x00);
            processor.NextStep();

            Assert.That(processor.SR.CarryFlag, Is.EqualTo(false));
        }

        [Test]
        public void CLD_Carry_Flag_Set_And_Cleared_Correctly()
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xF8, 0xD8 }, 0x00);
            processor.NextStep();
            processor.NextStep();

            Assert.That(processor.SR.DecimalFlag, Is.EqualTo(false));
        }

        [Test]
        public void CLI_Interrupt_Flag_Cleared_Correctly()
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0x58 }, 0x00);
            processor.NextStep();

            Assert.That(processor.SR.DisableInterruptsFlag, Is.EqualTo(false));
        }

        [Test]
        public void CLV_Overflow_Flag_Cleared_Correctly()
        {
            var processor = new Processor();

            processor.LoadProgram(0, new byte[] { 0xB8 }, 0x00);
            processor.NextStep();

            Assert.That(processor.SR.OverflowFlag, Is.EqualTo(false));
        }


    }
}
