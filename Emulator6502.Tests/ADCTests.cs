namespace Emulator6502.Tests
{
    [TestFixture]
    public class ADCTests
    {
        [TestCase(0, 0, false, 0)]
        [TestCase(0, 1, false, 1)]
        [TestCase(1, 2, false, 3)]
        [TestCase(255, 1, false, 0)]
        [TestCase(254, 1, false, 255)]
        [TestCase(255, 0, false, 255)]
        [TestCase(0, 0, true, 1)]
        [TestCase(0, 1, true, 2)]
        [TestCase(1, 2, true, 4)]
        [TestCase(254, 1, true, 0)]
        [TestCase(253, 1, true, 255)]
        [TestCase(254, 0, true, 255)]
        [TestCase(255, 255, true, 255)]
        public void ADC_Accumulator_Correct_When_Not_In_BDC_Mode(byte accumulatorInitialValue, byte amountToAdd,
            bool carryFlagSet, byte expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.Accumulator, Is.EqualTo(0x00));

            if (carryFlagSet)
            {
                processor.LoadProgram(0, new byte[] { 0x38, 0xA9, accumulatorInitialValue, 0x69, amountToAdd }, 0x00);
                processor.NextStep();
            }
            else
            {
                processor.LoadProgram(0, new byte[] { 0xA9, accumulatorInitialValue, 0x69, amountToAdd }, 0x00);
            }

            processor.NextStep();
            Assert.That(processor.Accumulator, Is.EqualTo(accumulatorInitialValue));

            processor.NextStep();
            Assert.That(processor.Accumulator, Is.EqualTo(expectedValue));
        }

        /*
        [TestCase(0x99, 0x99, false, 0x98)]
        [TestCase(0x99, 0x99, true, 0x99)]
        [TestCase(0x90, 0x99, false, 0x89)]
        public void ADC_Accumulator_Correct_When_In_BDC_Mode(byte accumulatorInitialValue, byte amountToAdd,
            bool setCarryFlag, byte expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.Accumulator, Is.EqualTo(0x00));

            if (setCarryFlag)
            {
                processor.LoadProgram(0, new byte[] { 0x38, 0xF8, 0xA9, accumulatorInitialValue, 0x69, amountToAdd },
                    0x00);
                processor.NextStep();
            }
            else
            {
                processor.LoadProgram(0, new byte[] { 0xF8, 0xA9, accumulatorInitialValue, 0x69, amountToAdd }, 0x00);
            }

            processor.NextStep();
            processor.NextStep();
            Assert.That(processor.Accumulator, Is.EqualTo(accumulatorInitialValue));

            processor.NextStep();
            Assert.That(processor.Accumulator, Is.EqualTo(expectedValue));
        }
        */
        // TODO: add decimal mode

        [TestCase(254, 1, false, false)]
        [TestCase(254, 1, true, true)]
        [TestCase(253, 1, true, false)]
        [TestCase(255, 1, false, true)]
        [TestCase(255, 1, true, true)]
        public void ADC_Carry_Correct_When_Not_In_BDC_Mode(byte accumulatorInitialValue, byte amountToAdd,
            bool setCarryFlag,
            bool expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.Accumulator, Is.EqualTo(0x00));

            if (setCarryFlag)
            {
                processor.LoadProgram(0, new byte[] { 0x38, 0xA9, accumulatorInitialValue, 0x69, amountToAdd }, 0x00);
                processor.NextStep();
            }
            else
            {
                processor.LoadProgram(0, new byte[] { 0xA9, accumulatorInitialValue, 0x69, amountToAdd }, 0x00);
            }

            processor.NextStep();
            Assert.That(processor.Accumulator, Is.EqualTo(accumulatorInitialValue));

            processor.NextStep();
            Assert.That(processor.SR.CarryFlag, Is.EqualTo(expectedValue));
        }

        /*
        [TestCase(98, 1, false, false)]
        [TestCase(98, 1, true, false)]
        [TestCase(99, 1, false, false)]
        [TestCase(99, 1, true, false)]
        public void ADC_Carry_Correct_When_In_BDC_Mode(byte accumulatorInitialValue, byte amountToAdd,
            bool setCarryFlag,
            bool expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.Accumulator, Is.EqualTo(0x00));

            processor.LoadProgram(0, new byte[] { 0xF8, 0xA9, accumulatorInitialValue, 0x69, amountToAdd }, 0x00);

            processor.NextStep();
            processor.NextStep();
            Assert.That(processor.Accumulator, Is.EqualTo(accumulatorInitialValue));

            processor.NextStep();
            Assert.That(processor.SR.CarryFlag, Is.EqualTo(expectedValue));
        }
        */
        // TODO: add decimal mode

        [TestCase(0, 0, true)]
        [TestCase(255, 1, true)]
        [TestCase(0, 1, false)]
        [TestCase(1, 0, false)]
        public void ADC_Zero_Flag_Correct_When_Not_In_BDC_Mode(byte accumulatorInitialValue, byte amountToAdd,
            bool expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.Accumulator, Is.EqualTo(0x00));

            processor.LoadProgram(0, new byte[] { 0xA9, accumulatorInitialValue, 0x69, amountToAdd }, 0x00);

            processor.NextStep();
            Assert.That(processor.Accumulator, Is.EqualTo(accumulatorInitialValue));

            processor.NextStep();
            Assert.That(processor.SR.ZeroFlag, Is.EqualTo(expectedValue));
        }

        [TestCase(126, 1, false)]
        [TestCase(1, 126, false)]
        [TestCase(1, 127, true)]
        [TestCase(127, 1, true)]
        [TestCase(1, 254, true)]
        [TestCase(254, 1, true)]
        [TestCase(1, 255, false)]
        [TestCase(255, 1, false)]
        public void ADC_Negative_Flag_Correct(byte accumulatorInitialValue, byte amountToAdd, bool expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.Accumulator, Is.EqualTo(0x00));


            processor.LoadProgram(0, new byte[] { 0xA9, accumulatorInitialValue, 0x69, amountToAdd }, 0x00);

            processor.NextStep();
            Assert.That(processor.Accumulator, Is.EqualTo(accumulatorInitialValue));

            processor.NextStep();
            Assert.That(processor.SR.NegativeFlag, Is.EqualTo(expectedValue));
        }

        [TestCase(0, 127, false, false)]
        [TestCase(0, 128, false, false)]
        [TestCase(1, 127, false, true)]
        [TestCase(1, 128, false, false)]
        [TestCase(127, 1, false, true)]
        [TestCase(127, 127, false, true)]
        [TestCase(128, 127, false, false)]
        [TestCase(128, 128, false, true)]
        [TestCase(128, 129, false, true)]
        [TestCase(128, 255, false, true)]
        [TestCase(255, 0, false, false)]
        [TestCase(255, 1, false, false)]
        [TestCase(255, 127, false, false)]
        [TestCase(255, 128, false, true)]
        [TestCase(255, 255, false, false)]
        [TestCase(0, 127, true, true)]
        [TestCase(0, 128, true, false)]
        [TestCase(1, 127, true, true)]
        [TestCase(1, 128, true, false)]
        [TestCase(127, 1, true, true)]
        [TestCase(127, 127, true, true)]
        [TestCase(128, 127, true, false)]
        [TestCase(128, 128, true, true)]
        [TestCase(128, 129, true, true)]
        [TestCase(128, 255, true, false)]
        [TestCase(255, 0, true, false)]
        [TestCase(255, 1, true, false)]
        [TestCase(255, 127, true, false)]
        [TestCase(255, 128, true, false)]
        [TestCase(255, 255, true, false)]
        public void ADC_Overflow_Flag_Correct(byte accumulatorInitialValue, byte amountToAdd, bool setCarryFlag,
            bool expectedValue)
        {
            var processor = new Processor();
            Assert.That(processor.Accumulator, Is.EqualTo(0x00));

            if (setCarryFlag)
            {
                processor.LoadProgram(0, new byte[] { 0x38, 0xA9, accumulatorInitialValue, 0x69, amountToAdd }, 0x00);
                processor.NextStep();
            }
            else
            {
                processor.LoadProgram(0, new byte[] { 0xA9, accumulatorInitialValue, 0x69, amountToAdd }, 0x00);
            }

            processor.NextStep();
            Assert.That(processor.Accumulator, Is.EqualTo(accumulatorInitialValue));

            processor.NextStep();
            Assert.That(processor.SR.OverflowFlag, Is.EqualTo(expectedValue));
        }
    }
}
