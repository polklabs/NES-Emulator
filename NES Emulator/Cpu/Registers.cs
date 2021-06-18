using System;
using System.Collections;
using ExtensionMethods;

namespace NES_Emulator
{
    struct status {
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
        public ushort PC;

        public status SR;
        public byte A;
        public byte X;
        public byte Y;
        public byte SP;

        public Registers()
        {
            SR = new status
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

            PC = 0;

            A = 0;
            X = 0;
            Y = 0;

            SP = 0xFA;
        }

        public byte SR_byte
        {
            get
            {                
                BitArray a = new BitArray(new bool[] { SR.C, SR.Z, SR.I, SR.D, SR.B, SR.u, SR.V, SR.N });
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
