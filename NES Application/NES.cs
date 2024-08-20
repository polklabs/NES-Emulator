using System;
using System.Collections.Generic;
using System.IO;

namespace NES_Application
{
    class NES
    {
        readonly Memory MEM;
        readonly Registers R;
        readonly Cpu CPU_6502;
        readonly GMemory GMEM;
        readonly Ppu PPU;

        public NES(string romName, Form1 form)
        {
            List<byte> prg = new List<byte>(); // Program
            List<byte> chr = new List<byte>();

            LoadRom(romName, prg, chr);

            MEM = new Memory(prg);
            R = new Registers
            {
                // Load the initial address into the program counter
                PC = (ushort)(MEM[0xFFFC] + (MEM[0xFFFD] << 8))
            };

            CPU_6502 = new Cpu(MEM, R);

            GMEM = new GMemory(chr);
            PPU = new Ppu(MEM, GMEM, form);
        }

        public IEnumerable<int> Run()
        {
            while(true)
            {
                bool opResult = CPU_6502.PerformOp();
                if (!opResult) break;
                PPU.memoryToFlags();
                PPU.Run();
                yield return 0;
            }

            RegisterPrint();
            MemoryDump();
        }

        public void RegisterPrint()
        {
            R.PrintRegisterStates();
        }

        public void MemoryDump()
        {
            byte[] memoryDump = new byte[0xFFFF];
            for (int i = 0; i < 0xFFFF; i++)
            {
                memoryDump[i] = MEM[i];
            }

            File.WriteAllBytes("dump.bin", memoryDump);

            byte[] gMemoryDump = new byte[0x3FFF];
            for (int i = 0; i < 0x3FFF; i++)
            {
                gMemoryDump[i] = GMEM[i];
            }

            File.WriteAllBytes("dumpG.bin", gMemoryDump);
        }

        private void LoadRom(string file, List<byte> PRG, List<byte> CHR)
        {
            byte[] data = File.ReadAllBytes(file);

            byte[] header = new byte[16];
            Array.Copy(data, header, 16);

            int prgLength = 1024 * 16 * header[4]; // # of 16KB blocks
            int chrLength = 1024 * 8 * header[5]; // # of 8KB blocks

            byte[] prg = new byte[prgLength];
            Array.Copy(data, 16, prg, 0, prgLength);

            byte[] chr = new byte[chrLength];
            Array.Copy(data, 16 + prgLength, chr, 0, chrLength);

            PRG.AddRange(prg);
            CHR.AddRange(chr);
        }
    }
}
