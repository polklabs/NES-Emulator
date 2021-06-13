using System;
using System.Collections.Generic;
using ExtensionMethods;

namespace NES_Emulator
{
    class NES
    {
        readonly Memory MEM;
        readonly Registers R;

        public NES(List<byte> prg, List<byte> chr)
        {
            MEM = new Memory(prg);
            R = new Registers
            {
                // Load the initial address into the program counter
                PC = Convert.ToUInt16((MEM[0xFFFD] << 8) + MEM[0xFFFC])
            };
        }

        void PushStack(byte v)
        {
            MEM[0x0100 + R.SP] = v;
            R.SP--;
        }

        void PushStack(ushort v)
        {
            PushStack((byte)R.PC);
            PushStack((byte)(R.PC >> 8));
        }

        byte PopStack()
        {
            R.SP++;
            return MEM[0x0100 + R.SP];            
        }

        //ushort PopStack()
        //{
        //    byte HH = PopStack();
        //    byte LL = PopStack();
        //    return Convert.ToUInt16((HH << 8) + LL);
        //}

        ushort GetPrgVal16()
        {            
            ushort val = Convert.ToUInt16((MEM[R.PC + 2] << 8) + MEM[R.PC + 1]);
            R.PC += 2;
            return val;
        }

        byte GetPrgVal8()
        {
            byte val = MEM[R.PC+1];
            R.PC++;
            return val;
        }

        public void Run()
        {
            while(true)
            {
                ushort tmpPC = 0;
                byte tmpByte = 0;
                byte opCode = MEM[R.PC];
                Console.Write($"0x{R.PC_str} 0x{opCode.ToHex()}: ");
                switch (opCode)
                {
                    // 0x1-
                    case 0x10:
                        tmpByte = GetPrgVal8();
                        if (!R.SR.N)
                            R.PC += (ushort)(sbyte)tmpByte;
                        Console.WriteLine($"BPL #{tmpByte.ToHex()}");
                        break;

                    // 0x2-
                    case 0x20:
                        tmpPC = GetPrgVal16();
                        PushStack(R.PC);
                        R.PC = tmpPC;
                        Console.WriteLine($"JSR ${tmpPC.ToHex()}");
                        continue;

                    // 0x4-
                    case 0x4C:
                        tmpPC = GetPrgVal16();
                        R.PC = tmpPC;
                        Console.WriteLine($"JMP ${R.PC_str}");
                        continue;
                    
                    // 0x7-
                    case 0x78:
                        R.SR.I = true;
                        Console.WriteLine("SEI");
                        break;

                    // 0x8-
                    case 0x85:
                        tmpByte = GetPrgVal8();
                        MEM[tmpByte] = R.A;
                        Console.WriteLine($"STA $00{tmpByte.ToHex()}");
                        break;
                    case 0x86:
                        tmpByte = GetPrgVal8();
                        MEM[tmpByte] = R.X;
                        Console.WriteLine($"STX $00{tmpByte.ToHex()}");
                        break;
                    case 0x8D:
                        tmpPC = GetPrgVal16();
                        MEM[tmpPC] = R.A;
                        Console.WriteLine($"STA ${tmpPC.ToHex()}");
                        break;

                    // 0x9-
                    case 0x91:
                        tmpByte = GetPrgVal8();
                        tmpPC = Convert.ToUInt16((MEM[tmpByte+1] << 8) + MEM[tmpByte]);
                        MEM[tmpPC + tmpByte] = R.A;
                        Console.WriteLine($"STA #{tmpByte.ToHex()},{R.Y_str}");
                        break;
                    case 0x9A:
                        R.SP = R.X;
                        Console.WriteLine($"TXS");
                        break;

                    // 0xA-
                    case 0xA0:
                        R.PC++;
                        R.Y = MEM[R.PC];
                        Console.WriteLine($"LDY #{R.Y_str}");
                        break;
                    case 0xA2:
                        tmpByte = GetPrgVal8();
                        R.X = tmpByte;
                        R.SR.N = (sbyte)R.X < 0; // Set SR negative flag
                        R.SR.Z = R.X == 0; // Set SR zero flag
                        Console.WriteLine($"LDX #{R.X_str}");
                        break;
                    case 0xA9:
                        tmpByte = GetPrgVal8();
                        R.A = tmpByte;
                        R.SR.N = (sbyte)R.A < 0; // Set SR negative flag
                        R.SR.Z = R.A == 0; // Set SR zero flag
                        Console.WriteLine($"LDA #{R.A_str}");
                        break;
                    case 0xAD:
                        tmpPC = GetPrgVal16();
                        R.A = MEM[tmpPC];
                        R.SR.N = (sbyte)R.A < 0; // Set SR negative flag
                        R.SR.Z = R.A == 0; // Set SR zero flag
                        Console.WriteLine($"LDA ${tmpPC.ToHex()}");
                        break;

                    // 0xB-
                    case 0xB0:
                        tmpByte = GetPrgVal8();
                        if (R.SR.C)
                        {
                            R.PC += (ushort)(sbyte)tmpByte;
                        }
                        Console.WriteLine($"BCS #{tmpByte.ToHex()}");
                        break;
                    case 0xBD:
                        tmpPC = GetPrgVal16();
                        R.A = MEM[tmpPC+R.X];
                        Console.WriteLine($"LDA ${tmpPC.ToHex()},{R.X_str}");
                        break;

                    // 0xC-
                    case 0xC9:                        
                        tmpByte = GetPrgVal8();
                        R.SR.N = (((sbyte)R.A - (sbyte)tmpByte) & 0x80) != 0; // Negative flag
                        R.SR.Z = R.A == tmpByte; // Zero flag
                        R.SR.C = R.A >= tmpByte; // Carry flag
                        Console.WriteLine($"CMP #{tmpByte.ToHex()}");
                        break;
                    case 0xCA:
                        R.X--;
                        R.SR.N = (sbyte)R.X < 0;
                        R.SR.Z = R.X == 0;
                        Console.WriteLine($"DEX");
                        break;

                    // 0xD-
                    case 0xD0:
                        tmpByte = GetPrgVal8();
                        if (!R.SR.Z)
                        {
                            R.PC += (ushort)(sbyte)tmpByte;
                        }
                        Console.WriteLine($"BNE #{tmpByte.ToHex()}");
                        break;
                    case 0xD8:
                        R.SR.D = false;
                        Console.WriteLine("CLD");
                        break;

                    // 0xE-
                    case 0xE0:
                        tmpByte = GetPrgVal8();
                        R.SR.N = (((sbyte)R.X - (sbyte)tmpByte) & 0x80) != 0; // Negative flag
                        R.SR.Z = R.X == tmpByte; // Zero flag
                        R.SR.C = R.X >= tmpByte; // Carry flag
                        Console.WriteLine($"CPX #{tmpByte.ToHex()}");
                        break;

                    default:
                        Console.WriteLine($"Unknown op code: {opCode.ToHex()}");
                        return;
                }
                R.PC++;
            }
        }
    }
}
