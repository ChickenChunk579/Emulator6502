using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Jumps
{
    public class JSR : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("JSR", AddressingMode.Absolute, OpcodeEnum.JSR, 6),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            // push pc to stack
            cpu.PushPCToStack();

            // jump
            cpu.ProgramCounter = cpu.GetAddressForOperation(operation);
            
        }
    }
}
