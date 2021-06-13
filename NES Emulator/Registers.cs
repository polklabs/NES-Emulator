using System.Collections;

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

        public string SR_str
        {
            get
            {                
                BitArray a = new BitArray(new bool[] { SR.N, SR.V, SR.u, SR.B, SR.D, SR.I, SR.Z, SR.C });
                byte[] b = new byte[1];
                a.CopyTo(b, 0);
                return b[0].ToString("X2");
            }
        }

        public string PC_str { get { return PC.ToString("X4"); } }
        public string A_str { get { return A.ToString("X2"); } }
        public string X_str { get { return X.ToString("X2"); } }
        public string Y_str { get { return Y.ToString("X2"); } }
        public string SP_str { get { return SP.ToString("X2"); } }
    }
}
