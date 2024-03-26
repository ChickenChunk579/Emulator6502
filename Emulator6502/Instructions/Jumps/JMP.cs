using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Jumps
{
    public class JMP : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("JMP", AddressingMode.Absolute, OpcodeEnum.JMP_ABS, 3),
            new Operation("JMP", AddressingMode.Indirect, OpcodeEnum.JMP_IND, 5),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            if (operation.AddressingMode == AddressingMode.Absolute)
            {
                int addrToJumpTo = cpu.GetAddressForOperation(operation);
                cpu.ProgramCounter = addrToJumpTo;
            } else
            {
                operation.AddressingMode = AddressingMode.Absolute;
                cpu.ProgramCounter = cpu.GetAddressForOperation(operation);

                if ((cpu.ProgramCounter & 0xFF) == 0xFF)
                {
                    int address = cpu.ReadMemoryValue(cpu.ProgramCounter);

                    address += 256 * cpu.ReadMemoryValue(cpu.ProgramCounter - 255);
                
                    cpu.ProgramCounter = address;
                } else
                {
                    cpu.ProgramCounter = cpu.GetAddressForOperation(operation);
                }
            }
            
        }
    }
}
