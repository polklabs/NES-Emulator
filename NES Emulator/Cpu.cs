using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;

namespace NES_Emulator
{
    class Cpu
    {
        readonly Memory MEM;
        readonly Registers R;

        public Cpu (Memory m, Registers r)
        {
            MEM = m;
            R = r;
        }

        #region Addressing Modes
        public byte AM_Load1Byte()
        {
            byte BB = MEM[R.PC + 1];
            R.PC++;
            return BB;
        }
        public ushort AM_Load2Bytes()
        {
            ushort HHLL = Convert.ToUInt16((MEM[R.PC + 2] << 8) + MEM[R.PC + 1]);
            R.PC += 2;
            return HHLL;
        }


        public ushort AM_Absolute()
        {
            return AM_Load2Bytes();
        }
        public ushort AM_AbsoluteX() 
        {
            ushort HHLL = AM_Load2Bytes();
            HHLL += R.X;            
            return HHLL;
        }
        public ushort AM_AbsoluteY() 
        {
            ushort HHLL = AM_Load2Bytes();
            HHLL += R.Y;
            return HHLL;
        }

        public byte AM_Immediate()
        {
            return AM_Load1Byte();
        }

        public ushort AM_Indirect()
        {
            ushort adr = AM_Load2Bytes();
            return Convert.ToUInt16((MEM[adr + 1] << 8) + MEM[adr]);
        }
        public ushort AM_XIndirect()
        {            
            byte LL = (byte)(AM_Load1Byte() + R.X);
            ushort HHLL = (ushort)(0x0000 + LL);            
            return Convert.ToUInt16((MEM[HHLL + 1] << 8) + MEM[HHLL]);
        }
        public ushort AM_IndirectY()
        {
            ushort HHLL = (ushort)(0x0000 + AM_Load1Byte());
            ushort word = Convert.ToUInt16((MEM[HHLL + 1] << 8) + MEM[HHLL]);
            word += R.Y;            
            return word;
        }

        public byte AM_Relative() { return AM_Immediate(); }

        public ushort AM_Zeropage()
        {
            return (ushort)(0x0000 + AM_Load1Byte());
        }

        public ushort AM_ZeropageX()
        {
            byte LL = AM_Load1Byte();
            LL += R.X;
            return (ushort)(0x0000 + LL);
        }

        public ushort AM_ZeropageY()
        {
            byte LL = AM_Load1Byte();
            LL += R.Y;
            return (ushort)(0x0000 + LL);
        }
        #endregion

        #region Stack
        void PushStack(byte v)
        {
            MEM[0x0100 + R.SP] = v;
            R.SP--;
        }

        byte PopStack()
        {
            R.SP++;
            return MEM[0x0100 + R.SP];
        }
        #endregion

        public bool PerformOp()
        {
            ushort tmpPC;
            byte tmpByte;
            byte opCode = MEM[R.PC];
            Console.Write($"0x{R.PC.ToHex()} {opCode.ToHex()}: ");
            switch (opCode)
            {
                //case 0x00:
                //	Console.WriteLine($"");
                //	break;
                //case 0x01:
                //	Console.WriteLine($"");
                //	break;
                //case 0x02:
                //	Console.WriteLine($"");
                //	break;
                //case 0x03:
                //	Console.WriteLine($"");
                //	break;
                //case 0x04:
                //	Console.WriteLine($"");
                //	break;
                //case 0x05:
                //	Console.WriteLine($"");
                //	break;
                //case 0x06:
                //	Console.WriteLine($"");
                //	break;
                //case 0x07:
                //	Console.WriteLine($"");
                //	break;
                //case 0x08:
                //	Console.WriteLine($"");
                //	break;
                //case 0x09:
                //	Console.WriteLine($"");
                //	break;
                //case 0x0a:
                //	Console.WriteLine($"");
                //	break;
                //case 0x0b:
                //	Console.WriteLine($"");
                //	break;
                //case 0x0c:
                //	Console.WriteLine($"");
                //	break;
                //case 0x0d:
                //	Console.WriteLine($"");
                //	break;
                //case 0x0e:
                //	Console.WriteLine($"");
                //	break;
                //case 0x0f:
                //	Console.WriteLine($"");
                //	break;
                case 0x10:
                    tmpByte = AM_Relative();
                    if (!R.SR.N) R.PC += (ushort)(sbyte)tmpByte;
                    Console.WriteLine($"BPL #{tmpByte.ToHex()}");
                    break;
                //case 0x11:
                //	Console.WriteLine($"");
                //	break;
                //case 0x12:
                //	Console.WriteLine($"");
                //	break;
                //case 0x13:
                //	Console.WriteLine($"");
                //	break;
                //case 0x14:
                //	Console.WriteLine($"");
                //	break;
                //case 0x15:
                //	Console.WriteLine($"");
                //	break;
                //case 0x16:
                //	Console.WriteLine($"");
                //	break;
                //case 0x17:
                //	Console.WriteLine($"");
                //	break;
                case 0x18:
                    R.SR.C = false;
                    Console.WriteLine($"CLC");
                    break;
                //case 0x19:
                //	Console.WriteLine($"");
                //	break;
                //case 0x1a:
                //	Console.WriteLine($"");
                //	break;
                //case 0x1b:
                //	Console.WriteLine($"");
                //	break;
                //case 0x1c:
                //	Console.WriteLine($"");
                //	break;
                //case 0x1d:
                //	Console.WriteLine($"");
                //	break;
                //case 0x1e:
                //	Console.WriteLine($"");
                //	break;
                //case 0x1f:
                //	Console.WriteLine($"");
                //	break;
                case 0x20:
                    tmpPC = AM_Absolute();
                    PushStack((byte)R.PC);
                    PushStack((byte)(R.PC >> 8));
                    R.PC = tmpPC;
                    Console.WriteLine($"JSR ${tmpPC.ToHex()}");
                    return true;
                //case 0x21:
                //	Console.WriteLine($"");
                //	break;
                //case 0x22:
                //	Console.WriteLine($"");
                //	break;
                //case 0x23:
                //	Console.WriteLine($"");
                //	break;
                //case 0x24:
                //	Console.WriteLine($"");
                //	break;
                //case 0x25:
                //	Console.WriteLine($"");
                //	break;
                //case 0x26:
                //	Console.WriteLine($"");
                //	break;
                //case 0x27:
                //	Console.WriteLine($"");
                //	break;
                //case 0x28:
                //	Console.WriteLine($"");
                //	break;
                //case 0x29:
                //	Console.WriteLine($"");
                //	break;
                //case 0x2a:
                //	Console.WriteLine($"");
                //	break;
                //case 0x2b:
                //	Console.WriteLine($"");
                //	break;
                //case 0x2c:
                //	Console.WriteLine($"");
                //	break;
                //case 0x2d:
                //	Console.WriteLine($"");
                //	break;
                //case 0x2e:
                //	Console.WriteLine($"");
                //	break;
                //case 0x2f:
                //	Console.WriteLine($"");
                //	break;
                //case 0x30:
                //	Console.WriteLine($"");
                //	break;
                //case 0x31:
                //	Console.WriteLine($"");
                //	break;
                //case 0x32:
                //	Console.WriteLine($"");
                //	break;
                //case 0x33:
                //	Console.WriteLine($"");
                //	break;
                //case 0x34:
                //	Console.WriteLine($"");
                //	break;
                //case 0x35:
                //	Console.WriteLine($"");
                //	break;
                //case 0x36:
                //	Console.WriteLine($"");
                //	break;
                //case 0x37:
                //	Console.WriteLine($"");
                //	break;
                //case 0x38:
                //	Console.WriteLine($"");
                //	break;
                //case 0x39:
                //	Console.WriteLine($"");
                //	break;
                //case 0x3a:
                //	Console.WriteLine($"");
                //	break;
                //case 0x3b:
                //	Console.WriteLine($"");
                //	break;
                //case 0x3c:
                //	Console.WriteLine($"");
                //	break;
                //case 0x3d:
                //	Console.WriteLine($"");
                //	break;
                //case 0x3e:
                //	Console.WriteLine($"");
                //	break;
                //case 0x3f:
                //	Console.WriteLine($"");
                //	break;
                //case 0x40:
                //	Console.WriteLine($"");
                //	break;
                //case 0x41:
                //	Console.WriteLine($"");
                //	break;
                //case 0x42:
                //	Console.WriteLine($"");
                //	break;
                //case 0x43:
                //	Console.WriteLine($"");
                //	break;
                //case 0x44:
                //	Console.WriteLine($"");
                //	break;
                //case 0x45:
                //	Console.WriteLine($"");
                //	break;
                //case 0x46:
                //	Console.WriteLine($"");
                //	break;
                //case 0x47:
                //	Console.WriteLine($"");
                //	break;
                //case 0x48:
                //	Console.WriteLine($"");
                //	break;
                //case 0x49:
                //	Console.WriteLine($"");
                //	break;
                //case 0x4a:
                //	Console.WriteLine($"");
                //	break;
                //case 0x4b:
                //	Console.WriteLine($"");
                //	break;
                case 0x4c:
                    R.PC = AM_Absolute();
                    Console.WriteLine($"JMP ${R.PC.ToHex()}");
                    return true;
                //case 0x4d:
                //	Console.WriteLine($"");
                //	break;
                //case 0x4e:
                //	Console.WriteLine($"");
                //	break;
                //case 0x4f:
                //	Console.WriteLine($"");
                //	break;
                //case 0x50:
                //	Console.WriteLine($"");
                //	break;
                //case 0x51:
                //	Console.WriteLine($"");
                //	break;
                //case 0x52:
                //	Console.WriteLine($"");
                //	break;
                //case 0x53:
                //	Console.WriteLine($"");
                //	break;
                //case 0x54:
                //	Console.WriteLine($"");
                //	break;
                //case 0x55:
                //	Console.WriteLine($"");
                //	break;
                //case 0x56:
                //	Console.WriteLine($"");
                //	break;
                //case 0x57:
                //	Console.WriteLine($"");
                //	break;
                //case 0x58:
                //	Console.WriteLine($"");
                //	break;
                //case 0x59:
                //	Console.WriteLine($"");
                //	break;
                //case 0x5a:
                //	Console.WriteLine($"");
                //	break;
                //case 0x5b:
                //	Console.WriteLine($"");
                //	break;
                //case 0x5c:
                //	Console.WriteLine($"");
                //	break;
                //case 0x5d:
                //	Console.WriteLine($"");
                //	break;
                //case 0x5e:
                //	Console.WriteLine($"");
                //	break;
                //case 0x5f:
                //	Console.WriteLine($"");
                //	break;
                //case 0x60:
                //	Console.WriteLine($"");
                //	break;
                //case 0x61:
                //	Console.WriteLine($"");
                //	break;
                //case 0x62:
                //	Console.WriteLine($"");
                //	break;
                //case 0x63:
                //	Console.WriteLine($"");
                //	break;
                //case 0x64:
                //	Console.WriteLine($"");
                //	break;
                //case 0x65:
                //	Console.WriteLine($"");
                //	break;
                //case 0x66:
                //	Console.WriteLine($"");
                //	break;
                //case 0x67:
                //	Console.WriteLine($"");
                //	break;
                //case 0x68:
                //	Console.WriteLine($"");
                //	break;
                //case 0x69:
                //    tmpByte = AM_Immediate();
                //    R.A += tmpByte;
                //    R.A += (byte)(R.SR.C ? 1 : 0);
                //    R.SR.N = (sbyte)R.A < 0;
                //    R.SR.Z = R.A == 0; // Zero flag
                //    R.SR.C = R.Y >= tmpByte; // Carry flag
                //    Console.WriteLine($"ADC");
                //    break;
                //case 0x6a:
                //	Console.WriteLine($"");
                //	break;
                //case 0x6b:
                //	Console.WriteLine($"");
                //	break;
                //case 0x6c:
                //	Console.WriteLine($"");
                //	break;
                //case 0x6d:
                //	Console.WriteLine($"");
                //	break;
                //case 0x6e:
                //	Console.WriteLine($"");
                //	break;
                //case 0x6f:
                //	Console.WriteLine($"");
                //	break;
                //case 0x70:
                //	Console.WriteLine($"");
                //	break;
                //case 0x71:
                //	Console.WriteLine($"");
                //	break;
                //case 0x72:
                //	Console.WriteLine($"");
                //	break;
                //case 0x73:
                //	Console.WriteLine($"");
                //	break;
                //case 0x74:
                //	Console.WriteLine($"");
                //	break;
                //case 0x75:
                //	Console.WriteLine($"");
                //	break;
                //case 0x76:
                //	Console.WriteLine($"");
                //	break;
                //case 0x77:
                //	Console.WriteLine($"");
                //	break;
                case 0x78:
                    R.SR.I = true;
                    Console.WriteLine($"SEI");
                    break;
                //case 0x79:
                //	Console.WriteLine($"");
                //	break;
                //case 0x7a:
                //	Console.WriteLine($"");
                //	break;
                //case 0x7b:
                //	Console.WriteLine($"");
                //	break;
                //case 0x7c:
                //	Console.WriteLine($"");
                //	break;
                //case 0x7d:
                //	Console.WriteLine($"");
                //	break;
                //case 0x7e:
                //	Console.WriteLine($"");
                //	break;
                //case 0x7f:
                //	Console.WriteLine($"");
                //	break;
                //case 0x80:
                //	Console.WriteLine($"");
                //	break;
                //case 0x81:
                //	Console.WriteLine($"");
                //	break;
                //case 0x82:
                //	Console.WriteLine($"");
                //	break;
                //case 0x83:
                //	Console.WriteLine($"");
                //	break;
                case 0x84:
                    tmpPC = AM_Zeropage();
                    MEM[tmpPC] = R.Y;
                    Console.WriteLine($"STY ${tmpPC.ToHex()}");
                    break;
                case 0x85:
                    tmpPC = AM_Zeropage();
                    MEM[tmpPC] = R.A;
                    Console.WriteLine($"STA ${tmpPC.ToHex()}");
                    break;
                //case 0x86:
                //	Console.WriteLine($"");
                //	break;
                //case 0x87:
                //	Console.WriteLine($"");
                //	break;
                //case 0x88:
                //	Console.WriteLine($"");
                //	break;
                //case 0x89:
                //	Console.WriteLine($"");
                //	break;
                //case 0x8a:
                //	Console.WriteLine($"");
                //	break;
                //case 0x8b:
                //	Console.WriteLine($"");
                //	break;
                //case 0x8c:
                //	Console.WriteLine($"");
                //	break;
                case 0x8d:
                    tmpPC = AM_Absolute();
                    MEM[tmpPC] = R.A;
                    Console.WriteLine($"STA ${tmpPC.ToHex()}");
                    break;
                //case 0x8e:
                //	Console.WriteLine($"");
                //	break;
                //case 0x8f:
                //	Console.WriteLine($"");
                //	break;
                //case 0x90:
                //	Console.WriteLine($"");
                //	break;
                case 0x91:
                    tmpPC = AM_IndirectY();
                    MEM[tmpPC] = R.A;
                    Console.WriteLine($"STA (ind),Y ${tmpPC.ToHex()}");
                    break;
                //case 0x92:
                //	Console.WriteLine($"");
                //	break;
                //case 0x93:
                //	Console.WriteLine($"");
                //	break;
                //case 0x94:
                //	Console.WriteLine($"");
                //	break;
                //case 0x95:
                //	Console.WriteLine($"");
                //	break;
                //case 0x96:
                //	Console.WriteLine($"");
                //	break;
                //case 0x97:
                //	Console.WriteLine($"");
                //	break;
                case 0x98:
                    R.A = R.Y;
                    R.SR.N = (sbyte)R.A < 0;
                    R.SR.Z = R.A == 0;
                    Console.WriteLine($"TYA");
                    break;
                case 0x99:
                    tmpPC = AM_AbsoluteY();
                    MEM[tmpPC] = R.A;
                    Console.WriteLine($"STA ${tmpPC.ToHex()},Y");
                    break;
                case 0x9a:
                    R.SP = R.X;
                    Console.WriteLine($"TXS");
                    break;
                //case 0x9b:
                //	Console.WriteLine($"");
                //	break;
                //case 0x9c:
                //	Console.WriteLine($"");
                //	break;
                //case 0x9d:
                //	Console.WriteLine($"");
                //	break;
                //case 0x9e:
                //	Console.WriteLine($"");
                //	break;
                //case 0x9f:
                //	Console.WriteLine($"");
                //	break;
                case 0xa0:
                    R.Y = AM_Immediate();
                    R.SR.N = (sbyte)R.Y < 0;
                    R.SR.Z = R.Y == 0;
                    Console.WriteLine($"LDY #{R.Y.ToHex()}");
                    break;
                //case 0xa1:
                //	Console.WriteLine($"");
                //	break;
                case 0xa2:
                    R.X = AM_Immediate();
                    R.SR.N = (sbyte)R.X < 0;
                    R.SR.Z = R.X == 0;
                    Console.WriteLine($"LDX #{R.X.ToHex()}");
                    break;
                //case 0xa3:
                //	Console.WriteLine($"");
                //	break;
                //case 0xa4:
                //	Console.WriteLine($"");
                //	break;
                //case 0xa5:
                //	Console.WriteLine($"");
                //	break;
                //case 0xa6:
                //	Console.WriteLine($"");
                //	break;
                //case 0xa7:
                //	Console.WriteLine($"");
                //	break;
                case 0xa8:
                    R.Y = R.A;
                    R.SR.N = (sbyte)R.Y < 0;
                    R.SR.Z = R.Y == 0;
                    Console.WriteLine($"TAY");
                    break;
                case 0xa9:
                    R.A = AM_Immediate();
                    R.SR.N = (sbyte)R.A < 0;
                    R.SR.Z = R.A == 0;
                    Console.WriteLine($"LDA #{R.A.ToHex()}");
                    break;
                //case 0xaa:
                //	Console.WriteLine($"");
                //	break;
                //case 0xab:
                //	Console.WriteLine($"");
                //	break;
                //case 0xac:
                //	Console.WriteLine($"");
                //	break;
                case 0xad:
                    tmpPC = AM_Absolute();
                    R.A = MEM[tmpPC];
                    R.SR.N = (sbyte)R.A < 0;
                    R.SR.Z = R.A == 0;
                    Console.WriteLine($"LDA ${tmpPC.ToHex()}");
                    break;
                //case 0xae:
                //	Console.WriteLine($"");
                //	break;
                //case 0xaf:
                //	Console.WriteLine($"");
                //	break;
                //case 0xb0:
                //	Console.WriteLine($"");
                //	break;
                //case 0xb1:
                //	Console.WriteLine($"");
                //	break;
                //case 0xb2:
                //	Console.WriteLine($"");
                //	break;
                //case 0xb3:
                //	Console.WriteLine($"");
                //	break;
                //case 0xb4:
                //	Console.WriteLine($"");
                //	break;
                //case 0xb5:
                //	Console.WriteLine($"");
                //	break;
                //case 0xb6:
                //	Console.WriteLine($"");
                //	break;
                //case 0xb7:
                //	Console.WriteLine($"");
                //	break;
                //case 0xb8:
                //	Console.WriteLine($"");
                //	break;
                case 0xb9:
                    tmpPC = AM_AbsoluteY();
                    R.A = MEM[tmpPC];
                    R.SR.N = (sbyte)R.A < 0;
                    R.SR.Z = R.A == 0;
                    Console.WriteLine($"LDA abs,Y ${tmpPC.ToHex()}");
                    break;
                //case 0xba:
                //	Console.WriteLine($"");
                //	break;
                //case 0xbb:
                //	Console.WriteLine($"");
                //	break;
                //case 0xbc:
                //	Console.WriteLine($"");
                //	break;
                //case 0xbd:
                //	Console.WriteLine($"");
                //	break;
                //case 0xbe:
                //	Console.WriteLine($"");
                //	break;
                //case 0xbf:
                //	Console.WriteLine($"");
                //	break;
                case 0xc0:
                    tmpByte = AM_Immediate();
                    R.SR.N = (((sbyte)R.Y - (sbyte)tmpByte) & 0x80) != 0; // Negative flag
                    R.SR.Z = R.Y == tmpByte; // Zero flag
                    R.SR.C = R.Y >= tmpByte; // Carry flag
                    Console.WriteLine($"CPY #{tmpByte.ToHex()}");
                    break;
                //case 0xc1:
                //	Console.WriteLine($"");
                //	break;
                //case 0xc2:
                //	Console.WriteLine($"");
                //	break;
                //case 0xc3:
                //	Console.WriteLine($"");
                //	break;
                //case 0xc4:
                //	Console.WriteLine($"");
                //	break;
                case 0xc5:
                    tmpPC = AM_Zeropage();
                    tmpByte = MEM[tmpPC];
                    R.SR.N = (((sbyte)R.A - (sbyte)tmpByte) & 0x80) != 0; // Negative flag
                    R.SR.Z = R.A == tmpByte; // Zero flag
                    R.SR.C = R.A >= tmpByte; // Carry flag
                    Console.WriteLine($"CMP ${tmpPC.ToHex()}");
                    break;
                //case 0xc6:
                //	Console.WriteLine($"");
                //	break;
                //case 0xc7:
                //	Console.WriteLine($"");
                //	break;
                case 0xc8:
                    R.Y++;
                    R.SR.N = (sbyte)R.Y < 0;
                    R.SR.Z = R.Y == 0;
                    Console.WriteLine($"INY");
                    break;
                //case 0xc9:
                //	Console.WriteLine($"");
                //	break;
                //case 0xca:
                //	Console.WriteLine($"");
                //	break;
                //case 0xcb:
                //	Console.WriteLine($"");
                //	break;
                //case 0xcc:
                //	Console.WriteLine($"");
                //	break;
                //case 0xcd:
                //	Console.WriteLine($"");
                //	break;
                //case 0xce:
                //	Console.WriteLine($"");
                //	break;
                //case 0xcf:
                //	Console.WriteLine($"");
                //	break;
                case 0xd0:
                    tmpByte = AM_Relative();
                    if (!R.SR.Z) R.PC += (ushort)(sbyte)tmpByte;
                    Console.WriteLine($"BNE #{tmpByte.ToHex()}");
                    break;
                //case 0xd1:
                //	Console.WriteLine($"");
                //	break;
                //case 0xd2:
                //	Console.WriteLine($"");
                //	break;
                //case 0xd3:
                //	Console.WriteLine($"");
                //	break;
                //case 0xd4:
                //	Console.WriteLine($"");
                //	break;
                //case 0xd5:
                //	Console.WriteLine($"");
                //	break;
                //case 0xd6:
                //	Console.WriteLine($"");
                //	break;
                //case 0xd7:
                //	Console.WriteLine($"");
                //	break;
                case 0xd8:
                    R.SR.D = false;
                    Console.WriteLine($"CLD");
                    break;
                case 0xd9:
                    tmpPC = AM_AbsoluteY();
                    tmpByte = MEM[tmpPC];
                    R.SR.N = (((sbyte)R.A - (sbyte)tmpByte) & 0x80) != 0; // Negative flag
                    R.SR.Z = R.A == tmpByte; // Zero flag
                    R.SR.C = R.A >= tmpByte; // Carry flag
                    Console.WriteLine($"CMP abs,Y ${tmpPC.ToHex()}");
                    break;
                //case 0xda:
                //	Console.WriteLine($"");
                //	break;
                //case 0xdb:
                //	Console.WriteLine($"");
                //	break;
                //case 0xdc:
                //	Console.WriteLine($"");
                //	break;
                //case 0xdd:
                //	Console.WriteLine($"");
                //	break;
                //case 0xde:
                //	Console.WriteLine($"");
                //	break;
                //case 0xdf:
                //	Console.WriteLine($"");
                //	break;
                //case 0xe0:
                //	Console.WriteLine($"");
                //	break;
                //case 0xe1:
                //	Console.WriteLine($"");
                //	break;
                //case 0xe2:
                //	Console.WriteLine($"");
                //	break;
                case 0xe4:
                    tmpPC = AM_Zeropage();
                    tmpByte = MEM[tmpPC];
                    R.SR.N = (((sbyte)R.X - (sbyte)tmpByte) & 0x80) != 0; // Negative flag
                    R.SR.Z = R.X == tmpByte; // Zero flag
                    R.SR.C = R.X >= tmpByte; // Carry flag
                    Console.WriteLine($"CPX ${tmpPC.ToHex()}");
                    break;
                //case 0xe5:
                //	Console.WriteLine($"");
                //	break;
                case 0xe6:
                    tmpPC = AM_Zeropage();
                    MEM[tmpPC]++;
                    Console.WriteLine($"INC ${tmpPC.ToHex()}");
                    break;
                //case 0xe8:
                //	Console.WriteLine($"");
                //	break;
                //case 0xe9:
                //	Console.WriteLine($"");
                //	break;
                //case 0xea:
                //	Console.WriteLine($"");
                //	break;
                //case 0xec:
                //	Console.WriteLine($"");
                //	break;
                //case 0xed:
                //	Console.WriteLine($"");
                //	break;
                //case 0xee:
                //	Console.WriteLine($"");
                //	break;
                //case 0xf0:
                //	Console.WriteLine($"");
                //	break;
                //case 0xf1:
                //	Console.WriteLine($"");
                //	break;
                //case 0xf2:
                //	Console.WriteLine($"");
                //	break;
                //case 0xf4:
                //	Console.WriteLine($"");
                //	break;
                //case 0xf5:
                //	Console.WriteLine($"");
                //	break;
                //case 0xf6:
                //	Console.WriteLine($"");
                //	break;
                //case 0xf8:
                //	Console.WriteLine($"");
                //	break;
                //case 0xf9:
                //	Console.WriteLine($"");
                //	break;
                //case 0xfa:
                //	Console.WriteLine($"");
                //	break;
                //case 0xfc:
                //	Console.WriteLine($"");
                //	break;
                //case 0xfd:
                //	Console.WriteLine($"");
                //	break;
                //case 0xfe:
                //	Console.WriteLine($"");
                //	break;


                default:
                    Console.WriteLine($"Unknown op code: {opCode.ToHex()}");
                    return false;
            }

            R.PC++;
            return true;
        }
    }
}
