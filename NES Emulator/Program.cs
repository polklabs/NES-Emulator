using System;
using System.Collections.Generic;
using System.IO;

namespace NES_Emulator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<byte> PRG = new List<byte>();
            List<byte> CHR = new List<byte>();

            LoadRom("Super Mario Bros (E).nes", PRG, CHR);

            NES nes = new NES(PRG, CHR);            
            nes.Run();

            Console.ReadLine();
        }

        static void LoadRom(string file, List<byte> PRG, List<byte> CHR)
        {
            byte[] data = File.ReadAllBytes(file);

            byte[] header = new byte[16];
            Array.Copy(data, header, 16);

            int prgLength = 1024 * 16 * header[4]; // # of 16KB blocks
            int chrLength = 1024 * 8 * header[5]; // # of 8KB blocks

            byte[] prg = new byte[prgLength];
            Array.Copy(data, 16, prg, 0, prgLength);

            byte[] chr = new byte[chrLength];
            Array.Copy(data, 16+prgLength, chr, 0, chrLength);

            PRG.AddRange(prg);
            CHR.AddRange(chr);
        }
    }
}
