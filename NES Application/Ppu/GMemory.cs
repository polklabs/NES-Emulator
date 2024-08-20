using System;
using System.Collections.Generic;

namespace NES_Application
{
    class GMemory
    {
        private readonly byte[][] CHR; // CHR-ROM banks 
        private readonly byte[] NTB = new byte[0x1000]; // Nametables
        private readonly byte[] PRI = new byte[0x0020]; // Palette RAM Indexes

        public GMemory(List<byte> chr)
        {
            var chrArray = chr.ToArray();
            var banks = chrArray.Length / 0x2000;
            CHR = new byte[banks][];
            for (int i = 0; i < banks; i++)
            {
                CHR[i] = new byte[0x2000];
                Array.Copy(chrArray, 0x2000*i, CHR[i], 0, 0x2000);
            }
        }

        public byte this[ushort address]
        {
            get => GetMemory(address);
            set => SetMemory(address, value);
        }

        public byte this[int address]
        {
            get => GetMemory((ushort)address);
            set => SetMemory((ushort)address, value);
        }

        public void SetMemory(ushort address, byte val)
        {
            byte[] memoryUnit = GetMemoryUnit(address);
            ushort offset = GetMemoryOffset(address);

            while (address - offset >= memoryUnit.Length)
                offset += (ushort)memoryUnit.Length;

            if (address - offset >= memoryUnit.Length)
                throw new Exception($"Address out of bounds: {address:X4}");

            // TODO: Handle paging
            memoryUnit[address - offset] = val;
        }

        public byte GetMemory(ushort address)
        {
            byte[] memoryUnit = GetMemoryUnit(address);
            ushort offset = GetMemoryOffset(address);

            while (address - offset >= memoryUnit.Length)
                offset += (ushort)memoryUnit.Length;

            if (address - offset >= memoryUnit.Length)
                throw new Exception($"Address out of bounds: {address:X4}");

            // TODO: Handle paging
            return memoryUnit[address - offset];
        }

        private byte[] GetMemoryUnit(ushort address)
        {
            if (address < 0x2000) return CHR[0];
            if (address < 0x3F00) return NTB;
            if (address <= 0x3FFF) return PRI;
            throw new Exception($"Unknown memory address: {address:X4}");
        }

        private static ushort GetMemoryOffset(ushort address)
        {
            if (address < 0x2000) return 0;
            if (address < 0x3F00) return 0x2000;
            if (address <= 0x3FFF) return 0x3F00;
            throw new Exception($"Unknown memory address: {address:X4}");
        }
    }
}
