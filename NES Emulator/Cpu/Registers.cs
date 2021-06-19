using System;
using System.Collections;
using ExtensionMethods;

namespace NES_Emulator
{
    struct Status {
        public bool N;
        public bool V;
        public bool u;
        public bool B;
        public bool D;
        public bool I;
        public bool Z;
        public bool C;
    }

    class Registers
    {
        public ushort PC = 0;

        public Status SR;
        public byte A = 0;
        public byte X = 0;
        public byte Y = 0;
        public byte SP = 0xFA;

        public Registers()
        {
            SR = new Status
            {
                N = false,
                V = false,
                u = true,
                B = true,
                D = false,
                I = true,
                Z = false,
                C = false
            };
        }

        public byte SR_byte
        {
            get
            {                
                BitArray a = new(new bool[] { SR.C, SR.Z, SR.I, SR.D, SR.B, SR.u, SR.V, SR.N });
                byte[] b = new byte[1];
                a.CopyTo(b, 0);
                return b[0];
            }
        }

        public void PrintRegisterStates()
        {
            Console.WriteLine("");
            Console.WriteLine($"PC: {PC.ToHex()}");
            Console.WriteLine($"SR: {SR_byte.ToHex()} - {Convert.ToString(SR_byte, 2).PadLeft(8, '0')}");
            Console.WriteLine($"A:  {A.ToHex()}");
            Console.WriteLine($"X:  {X.ToHex()}");
            Console.WriteLine($"Y:  {Y.ToHex()}");
            Console.WriteLine($"SP: {SP.ToHex()}");
        }

        
    }
}
