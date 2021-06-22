using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NES_Emulator
{
    class Ppu
    {
        readonly Memory MEM;
        readonly GMemory GMEM;

        bool V = false; // NMI Enable
        bool P = false; // PPU master/slave
        bool H = false; // Sprite Height
        bool B = false; // Background Tile Select
        bool S = false; // Sprite Tile Select
        bool I = false; // Increment Mode
        byte NN = 0x00; // Nametable select

        byte BGR = 0x00; // Color Emphasis
        bool s = false; // Sprite Enable
        bool b = false; // Background Enable
        bool M = false; // Sprite left column enable
        bool m = false; // Background left column enable
        bool G = false; // Greyscale

        public Ppu(Memory mem, GMemory gmem)
        {
            MEM = mem;
            GMEM = gmem;
        }

        public void memoryToFlags()
        {
            V = (MEM[0x2000] & 0x80) != 0;
            P = (MEM[0x2000] & 0x40) != 0;
            H = (MEM[0x2000] & 0x20) != 0;
            B = (MEM[0x2000] & 0x10) != 0;
            S = (MEM[0x2000] & 0x08) != 0;
            I = (MEM[0x2000] & 0x04) != 0;
            NN = (byte)(MEM[0x2000] & 0x03);

            BGR = (byte)(MEM[0x2001] & 0xE0);
            s = (MEM[0x2001] & 0x10) != 0;
            b = (MEM[0x2001] & 0x08) != 0;
            M = (MEM[0x2001] & 0x04) != 0;
            m = (MEM[0x2001] & 0x02) != 0;
            G = (MEM[0x2001] & 0x01) != 0;
        }
    }
}
