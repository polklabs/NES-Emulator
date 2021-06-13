using System;
using System.Collections.Generic;

namespace NES_Emulator
{
    class NES
    {
        readonly Memory MEM;
        readonly Registers R;
        readonly Cpu CPU; 

        public NES(List<byte> prg, List<byte> chr)
        {
            MEM = new Memory(prg);
            R = new Registers
            {
                // Load the initial address into the program counter
                PC = Convert.ToUInt16((MEM[0xFFFD] << 8) + MEM[0xFFFC])
            };

            CPU = new Cpu(MEM, R);
        }

        public void Run()
        {
            while(true)
            {
                bool opResult = CPU.PerformOp();
                if (!opResult) break;                
            }

            R.PrintRegisterStates();
        }
    }
}
