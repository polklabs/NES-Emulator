using System;
using System.Collections.Generic;

namespace NES_Emulator
{
    class NES
    {
        readonly Memory MEM;
        readonly Registers R;
        readonly Cpu CPU_6502; 

        public NES(List<byte> prg, List<byte> chr)
        {
            MEM = new Memory(prg);
            R = new Registers
            {
                // Load the initial address into the program counter
                PC = (ushort)(MEM[0xFFFC] + (MEM[0xFFFD] << 8))
            };

            CPU_6502 = new Cpu(MEM, R);
        }

        public void Run()
        {
            while(true)
            {
                bool opResult = CPU_6502.PerformOp();
                if (!opResult) break;                
            }

            R.PrintRegisterStates();
        }
    }
}
