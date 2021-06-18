using System;
using System.Collections.Generic;

namespace NES_Emulator
{
    class Memory
    {
        private readonly byte[] RAM = new byte[0x0800];
        private readonly byte[] PPU = new byte[0x0008];
        private readonly byte[] AIO = new byte[0x0018]; // APU & I/O
        private readonly byte[] AIF = new byte[0x0018]; // APU & I/O testing
        private readonly byte[] PRA = new byte[0x3FE0]; // Progam RAM
        private readonly byte[] PRG; // Program

        public Memory(List<byte> prg)
        {
            PRG = prg.ToArray();
            this[0x2002] = 0x80;
        }

        public byte this[ushort address]
        {
            get => getMemory(address);
            set => setMemory(address, value);
        }

        public byte this[int address]
        {
            get => getMemory((ushort)address);
            set => setMemory((ushort)address, value);
        }

        public void setMemory(ushort address, byte val)
        {
            byte[] memoryUnit = getMemoryUnit(address);
            ushort offset = getMemoryOffset(address);

            if (address - offset >= memoryUnit.Length)
                throw new Exception($"Address out of bounds: {address.ToString("X4")}");

            // TODO: Handle paging
            memoryUnit[address - offset] = val;
        }

        public byte getMemory(ushort address)
        {
            byte[] memoryUnit = getMemoryUnit(address);
            ushort offset = getMemoryOffset(address);

            while (address - offset > memoryUnit.Length)
                offset += (ushort)memoryUnit.Length;

            if (address - offset >= memoryUnit.Length)
                throw new Exception($"Address out of bounds: {address.ToString("X4")}");

            // TODO: Handle paging
            return memoryUnit[address - offset];
        }

        private byte[] getMemoryUnit(ushort address)
        {
            if (address < 0x2000) return RAM;
            if (address < 0x4000) return PPU;
            if (address < 0x4018) return AIO;
            if (address < 0x4020) return AIF;
            if (address < 0x8000) return PRA;
            if (address <= 0xFFFF) return PRG;
            throw new Exception($"Unknown memory address: {address.ToString("X4")}");
        }

        private ushort getMemoryOffset(ushort address)
        {
            if (address < 0x2000) return 0;
            if (address < 0x4000) return 0x2000;
            if (address < 0x4018) return 0x4000;
            if (address < 0x4020) return 0x4018;
            if (address < 0x8000) return 0x4020;
            if (address <= 0xFFFF) return 0x8000;
            throw new Exception($"Unknown memory address: {address.ToString("X4")}");
        }
    }
}
