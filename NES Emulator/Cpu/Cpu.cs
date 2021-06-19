﻿using System;
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

        public ushort LoadAddress(AddressModes am)
        {
            switch (am)
            {
                case AddressModes.a:
                case AddressModes.imm:
                case AddressModes.impl:
                case AddressModes.rel:
                    return 0x0000;
                case AddressModes.abs:
                    return AM_Absolute();
                case AddressModes.absX:
                    return AM_AbsoluteX();
                case AddressModes.absY:
                    return AM_AbsoluteY();                
                case AddressModes.ind:
                    return AM_Indirect();
                case AddressModes.Xind:
                    return AM_XIndirect();
                case AddressModes.indY:
                    return AM_IndirectY();                
                case AddressModes.zpg:
                    return AM_Zeropage();
                case AddressModes.zpgX:
                    return AM_ZeropageX();
                case AddressModes.zpgY:
                    return AM_ZeropageY();
                default:
                    throw new Exception($"Unknown Address Mode: {am}");
            }
        }

        public byte LoadData(AddressModes am, ushort address)
        {
            switch (am)
            {
                case AddressModes.a:
                    return R.A;
                case AddressModes.imm:
                case AddressModes.rel:
                    return MEM[++R.PC];
                case AddressModes.impl:
                    return 0x0000;                
                case AddressModes.abs:                    
                case AddressModes.absX:                    
                case AddressModes.absY:                    
                case AddressModes.ind:
                case AddressModes.Xind:
                case AddressModes.indY:
                case AddressModes.zpg:
                case AddressModes.zpgX:
                case AddressModes.zpgY:
                    return MEM[address];
                default:
                    throw new Exception($"Unknown Address Mode: {am}");
            }
        }


        public byte AM_Load1Byte()
        {
            return MEM[++R.PC];            
        }
        public ushort AM_Load2Bytes()
        {
            return (ushort)(MEM[++R.PC] + (MEM[++R.PC] << 8));
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

        private void SetFlagsNZ(byte a)
        {
            R.SR.Z = a == 0;
            R.SR.N = (sbyte)a < 0;
        }

        private byte Add(byte a, byte b)
        {
            int c = a + b;
            bool carry = (c > 0xFF);

            // Overflow check
            sbyte a_s = (sbyte)a;
            sbyte b_s = (sbyte)b;
            sbyte d = (sbyte)(a_s + b_s);

            // If a&b have same sign, but a/b have a different sign then d there is an overflow
            // https://codereview.stackexchange.com/a/107652
            bool overflow = (a_s ^ b_s) >= 0 && (a_s ^ d) < 0;

            byte r = (byte)c;

            SetFlagsNZ(r);
            R.SR.C = carry;
            R.SR.V = overflow;

            return r;
        }

        private byte Subtract(byte a, byte b)
        {
            int c = a - b;
            bool carry = (c & 0x100) == 0; // Because of signs we can't just compare to 0xFF so check the 9th bit for a 1

            // Overflow check
            sbyte a_s = (sbyte)a;
            sbyte b_s = (sbyte)b;
            sbyte d = (sbyte)(a_s - b_s);

            // If a&b have same sign, but a/b have a different sign then d there is an overflow
            // https://codereview.stackexchange.com/a/107652
            bool overflow = (a_s ^ (-b_s)) >= 0 && (a_s ^ d) < 0;

            byte r = (byte)c;

            SetFlagsNZ(r);
            R.SR.C = carry;
            R.SR.V = overflow;

            return r;
        }

        private void Compare(byte a, byte b)
        {
            R.SR.N = ((byte)(a - b) & 0x80) != 0; // Negative flag
            R.SR.Z = a == b; // Zero flag
            R.SR.C = a >= b; // Carry flag
        }

        public bool PerformOp()
        {
            byte code = MEM[R.PC];
            OpCode opCode = OpCode.GetOpCode(code);

            Console.Write($"0x{R.PC.ToHex()} {code.ToHex()}: ");

            if (R.PC == 0xC1D7) return false;

            ushort tmpAddr = LoadAddress(opCode.Addressing);
            byte tmpByte = LoadData(opCode.Addressing, tmpAddr);
            sbyte tmpSByte = (sbyte)tmpByte;
            
            Console.WriteLine(string.Format(opCode.Assembler, tmpAddr.ToHex(), tmpByte.ToHex(), tmpSByte));
            switch (code)
            {

                // ADC
                case 0x69:
                case 0x65:
                case 0x75:
                case 0x6D:
                case 0x7D:
                case 0x79:
                case 0x61:
                case 0x71:
                    R.A = Add(R.A, tmpByte);                    
                    break;

                // AND
                case 0x29:
                case 0x25:
                case 0x35:
                case 0x2D:
                case 0x3D:
                case 0x39:
                case 0x21:
                case 0x31:
                    R.A &= tmpByte;
                    SetFlagsNZ(R.A);
                    break;

                // BCC
                case 0x90:
                    if (!R.SR.C) R.PC += (ushort)tmpSByte;
                    break;

                // BCS
                case 0xB0:
                    if (R.SR.C) R.PC += (ushort)tmpSByte;
                    break;

                // BIT
                case 0x24:
                case 0x2C:
                    R.SR.N = (tmpByte & 0x80) != 0;
                    R.SR.V = (tmpByte & 0x40) != 0;
                    R.SR.Z = (R.A & tmpByte) == 0;
                    break;

                // BNE
                case 0xD0:
                    if (!R.SR.Z) R.PC += (ushort)tmpSByte;                                        
                    break;

                // BPL
                case 0x10:
                    if (!R.SR.N) R.PC += (ushort)tmpSByte;
                    break;

                // CLC
                case 0x18:
                    R.SR.C = false;
                    break;

                // CLD
                case 0xD8:
                    R.SR.D = false;                    
                    break;

                // CMP
                case 0xC9:
                case 0xC5:
                case 0xD5:
                case 0xCD:
                case 0xDD:
                case 0xD9:
                case 0xC1:
                case 0xD1:                    
                    Compare(R.A, tmpByte);
                    break;

                // CPX
                case 0xE0:
                case 0xE4:
                case 0xEC:
                    Compare(R.X, tmpByte);
                    break;

                // CPY
                case 0xC0:
                case 0xC4:
                case 0xCC:
                    Compare(R.Y, tmpByte);
                    break;


                // DEX
                case 0xCA:
                    SetFlagsNZ(--R.X);
                    break;

                // DEY
                case 0x88:
                    SetFlagsNZ(--R.Y);
                    break;

                // INC
                case 0xE6:
                case 0xF6:
                case 0xEE:
                case 0xFE:                    
                    SetFlagsNZ(++MEM[tmpAddr]);
                    break;

                // INY
                case 0xC8:
                    SetFlagsNZ(++R.Y);
                    break;

                // JMP
                case 0x4C:
                case 0x6C:
                    R.PC = tmpAddr;
                    //if (tmpAddr == 0x8057) return false; // Super Mario Bros inf loop
                    return true;

                // JSR
                case 0x20:                    
                    PushStack((byte)(R.PC >> 8)); // Push high byte
                    PushStack((byte)R.PC); // Push low byte                    
                    R.PC = tmpAddr;
                    return true;

                // LDA
                case 0xA9:
                case 0xA5:
                case 0xB5:
                case 0xAD:
                case 0xBD:
                case 0xB9:
                case 0xA1:
                case 0xB1:
                    R.A = tmpByte;
                    SetFlagsNZ(R.A);
                    break;

                // LDX
                case 0xA2:
                case 0xA6:
                case 0xB6:
                case 0xAE:
                case 0xBE:
                    R.X = tmpByte;
                    SetFlagsNZ(R.X);                    
                    break;

                // LDY
                case 0xA0:
                case 0xA4:
                case 0xB4:
                case 0xAC:
                case 0xBC:
                    R.Y = tmpByte;
                    SetFlagsNZ(R.Y);
                    break;

                // ORA
                case 0x09:
                case 0x05:
                case 0x15:
                case 0x0D:
                case 0x1D:
                case 0x19:
                case 0x01:
                case 0x11:
                    R.A |= tmpByte;
                    SetFlagsNZ(R.A);
                    break;

                // RTS
                case 0x60:
                    R.PC = (ushort)(PopStack() + (PopStack() << 8));
                    break;

                // SEI
                case 0x78:
                    R.SR.I = true;                    
                    break;

                // STA
                case 0x85:
                case 0x95:
                case 0x8D:
                case 0x9D:
                case 0x99:
                case 0x81:
                case 0x91:
                    MEM[tmpAddr] = R.A;
                    break;

                // STX
                case 0x86:
                case 0x96:
                case 0x8E:
                    MEM[tmpAddr] = R.X;
                    break;

                // STY
                case 0x84:
                case 0x94:
                case 0x8C:
                    MEM[tmpAddr] = R.Y;
                    break;

                // TAY
                case 0xA8:
                    R.Y = R.A;
                    SetFlagsNZ(R.Y);
                    break;

                // TXA
                case 0x8A:
                    R.A = R.X;
                    SetFlagsNZ(R.A);
                    break;

                // TXS
                case 0x9A:
                    R.SP = R.X;                    
                    break;

                // TYA
                case 0x98:
                    R.A = R.Y;
                    SetFlagsNZ(R.A);
                    break;

                default:
                    Console.WriteLine($"Not implemented: {code.ToHex()}");
                    return false;
            }

            R.PC++;
            return true;
        }
    }
}
