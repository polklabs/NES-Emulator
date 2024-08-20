using ExtensionMethods;
using System;
using System.Collections.Generic;

namespace NES_Application
{
    enum AddressModes
    {
        a,
        abs,
        absX,
        absY,
        imm,
        impl,
        ind,
        Xind,
        indY,
        rel,
        zpg,
        zpgX,
        zpgY
    }

    class OpCode
    {
        public static Dictionary<byte, OpCode> OPCODES = new Dictionary<byte, OpCode>();

        // ADC
        public static OpCode _69 = new OpCode(0x69, AddressModes.imm, "ADC #{1}", 2);
        public static OpCode _65 = new OpCode(0x65, AddressModes.zpg, "ADC Z{0}", 2);
        public static OpCode _75 = new OpCode(0x75, AddressModes.zpgX, "ADC Z{0},X", 2);
        public static OpCode _6D = new OpCode(0x6D, AddressModes.abs, "ADC ${0}", 3);
        public static OpCode _7D = new OpCode(0x7D, AddressModes.absX, "ADC ${0},X", 3);
        public static OpCode _79 = new OpCode(0x79, AddressModes.absY, "ADC ${0},Y", 3);
        public static OpCode _61 = new OpCode(0x61, AddressModes.Xind, "ADC (${0},X)", 2);
        public static OpCode _71 = new OpCode(0x71, AddressModes.indY, "ADC (${0}),Y", 2);

        // AND
        public static OpCode _29 = new OpCode(0x29, AddressModes.imm, "AND #{1}", 2);
        public static OpCode _25 = new OpCode(0x25, AddressModes.zpg, "AND Z{0}", 2);
        public static OpCode _35 = new OpCode(0x35, AddressModes.zpgX, "AND Z{0},X", 2);
        public static OpCode _2D = new OpCode(0x2D, AddressModes.abs, "AND ${0}", 3);
        public static OpCode _3D = new OpCode(0x3D, AddressModes.absX, "AND ${0},X", 3);
        public static OpCode _39 = new OpCode(0x39, AddressModes.absY, "AND ${0},Y", 3);
        public static OpCode _21 = new OpCode(0x21, AddressModes.Xind, "AND (${0},X)", 2);
        public static OpCode _31 = new OpCode(0x31, AddressModes.indY, "AND (${0}),Y", 2);

        // ASL
        public static OpCode _0A = new OpCode(0x0A, AddressModes.a, "ASL A", 1);
        public static OpCode _06 = new OpCode(0x06, AddressModes.zpg, "ASL Z{1}", 2);
        public static OpCode _16 = new OpCode(0x16, AddressModes.zpgX, "ASL Z{0},X", 2);
        public static OpCode _0E = new OpCode(0x0E, AddressModes.abs, "ASL ${0}", 3);
        public static OpCode _1E = new OpCode(0x1E, AddressModes.absX, "ASL ${0},X", 3);

        // BCC
        public static OpCode _90 = new OpCode(0x90, AddressModes.rel, "BCC ${2}", 2);

        // BCS
        public static OpCode _B0 = new OpCode(0xB0, AddressModes.rel, "BCS ${2}", 2);

        //BEQ
        public static OpCode _F0 = new OpCode(0xF0, AddressModes.rel, "BEQ ${2}", 2);

        //BIT
        public static OpCode _24 = new OpCode(0x24, AddressModes.zpg, "BIT Z{0}", 2);
        public static OpCode _2C = new OpCode(0x2C, AddressModes.abs, "BIT ${0}", 3);

        //BMI
        public static OpCode _30 = new OpCode(0x30, AddressModes.rel, "BMI ${2}", 2);

        //BNE
        public static OpCode _D0 = new OpCode(0xD0, AddressModes.rel, "BNE ${2}", 2);

        //BPL
        public static OpCode _10 = new OpCode(0x10, AddressModes.rel, "BPL ${2}", 2);

        //BRK
        public static OpCode _00 = new OpCode(0x00, AddressModes.impl, "BRK 00", 1);

        //BVC
        public static OpCode _50 = new OpCode(0x50, AddressModes.rel, "BVC ${2}", 2);

        //BVS
        public static OpCode _70 = new OpCode(0x70, AddressModes.rel, "BVS ${2}", 2);

        //CLC
        public static OpCode _18 = new OpCode(0x18, AddressModes.impl, "CLC", 1);

        //CLD
        public static OpCode _D8 = new OpCode(0xD8, AddressModes.impl, "CLD", 1);

        //CLI
        public static OpCode _58 = new OpCode(0x58, AddressModes.impl, "CLI", 1);

        //CLV
        public static OpCode _B8 = new OpCode(0xB8, AddressModes.impl, "CLV", 1);

        //CMP
        public static OpCode _C9 = new OpCode(0xC9, AddressModes.imm, "CMP #{1}", 2);
        public static OpCode _C5 = new OpCode(0xC5, AddressModes.zpg, "CMP Z{0}", 2);
        public static OpCode _D5 = new OpCode(0xD5, AddressModes.zpgX, "CMP Z{0},X", 2);
        public static OpCode _CD = new OpCode(0xCD, AddressModes.abs, "CMP ${0}", 3);
        public static OpCode _DD = new OpCode(0xDD, AddressModes.absX, "CMP ${0},X", 3);
        public static OpCode _D9 = new OpCode(0xD9, AddressModes.absY, "CMP ${0},Y", 3);
        public static OpCode _C1 = new OpCode(0xC1, AddressModes.Xind, "CMP (${0},X)", 2);
        public static OpCode _D1 = new OpCode(0xD1, AddressModes.indY, "CMP (${0}),Y", 2);

        //CPX
        public static OpCode _E0 = new OpCode(0xE0, AddressModes.imm, "CPX #{1}", 2);
        public static OpCode _E4 = new OpCode(0xE4, AddressModes.zpg, "CPX Z{0}", 2);
        public static OpCode _EC = new OpCode(0xEC, AddressModes.abs, "CPX ${0}", 3);

        //CPY
        public static OpCode _C0 = new OpCode(0xC0, AddressModes.imm, "CPY #{1}", 2);
        public static OpCode _C4 = new OpCode(0xC4, AddressModes.zpg, "CPY Z{0}", 2);
        public static OpCode _CC = new OpCode(0xCC, AddressModes.abs, "CPY ${0}", 3);

        //DEC
        public static OpCode _C6 = new OpCode(0xC6, AddressModes.zpg, "DEC Z{0}", 2);
        public static OpCode _D6 = new OpCode(0xD6, AddressModes.zpgX, "DEC Z{0},X", 2);
        public static OpCode _CE = new OpCode(0xCE, AddressModes.abs, "DEC ${0}", 3);
        public static OpCode _DE = new OpCode(0xDE, AddressModes.absX, "DEC ${0},X", 3);

        //DEX
        public static OpCode _CA = new OpCode(0xCA, AddressModes.impl, "DEX", 1);

        //DEY
        public static OpCode _88 = new OpCode(0x88, AddressModes.impl, "DEY", 1);

        //EOR
        public static OpCode _49 = new OpCode(0x49, AddressModes.imm, "EOR #{1}", 2);
        public static OpCode _45 = new OpCode(0x45, AddressModes.zpg, "EOR Z{0}", 2);
        public static OpCode _55 = new OpCode(0x55, AddressModes.zpgX, "EOR Z{0},X", 2);
        public static OpCode _4D = new OpCode(0x4D, AddressModes.abs, "EOR ${0}", 3);
        public static OpCode _5D = new OpCode(0x5D, AddressModes.absX, "EOR ${0},X", 3);
        public static OpCode _59 = new OpCode(0x59, AddressModes.absY, "EOR ${0},Y", 3);
        public static OpCode _41 = new OpCode(0x41, AddressModes.Xind, "EOR (${0},X)", 2);
        public static OpCode _51 = new OpCode(0x51, AddressModes.indY, "EOR (${0}),Y", 2);

        //INC
        public static OpCode _E6 = new OpCode(0xE6, AddressModes.zpg, "INC Z{0}", 2);
        public static OpCode _F6 = new OpCode(0xF6, AddressModes.zpgX, "INC Z{0},X", 2);
        public static OpCode _EE = new OpCode(0xEE, AddressModes.abs, "INC ${0}", 3);
        public static OpCode _FE = new OpCode(0xFE, AddressModes.absX, "INC ${0},X", 3);

        //INX
        public static OpCode _E8 = new OpCode(0xE8, AddressModes.impl, "INX", 1);

        //INY
        public static OpCode _C8 = new OpCode(0xC8, AddressModes.impl, "INY", 1);

        //JMP
        public static OpCode _4C = new OpCode(0x4C, AddressModes.abs, "JMP ${0}", 3);
        public static OpCode _6C = new OpCode(0x6C, AddressModes.ind, "JMP (${0})", 3);

        //JSR
        public static OpCode _20 = new OpCode(0x20, AddressModes.abs, "JSR ${0}", 3);

        //LDA
        public static OpCode _A9 = new OpCode(0xA9, AddressModes.imm, "LDA #{1}", 2);
        public static OpCode _A5 = new OpCode(0xA5, AddressModes.zpg, "LDA Z{0}\t\t{2}", 2);
        public static OpCode _B5 = new OpCode(0xB5, AddressModes.zpgX, "LDA Z{0},X", 2);
        public static OpCode _AD = new OpCode(0xAD, AddressModes.abs, "LDA ${0}", 3);
        public static OpCode _BD = new OpCode(0xBD, AddressModes.absX, "LDA ${0},X", 3);
        public static OpCode _B9 = new OpCode(0xB9, AddressModes.absY, "LDA ${0},Y", 3);
        public static OpCode _A1 = new OpCode(0xA1, AddressModes.Xind, "LDA (${0},X)", 2);
        public static OpCode _B1 = new OpCode(0xB1, AddressModes.indY, "LDA (${0}),Y", 2);

        //LDX
        public static OpCode _A2 = new OpCode(0xA2, AddressModes.imm, "LDX #{1}", 2);
        public static OpCode _A6 = new OpCode(0xA6, AddressModes.zpg, "LDX Z{0}", 2);
        public static OpCode _B6 = new OpCode(0xB6, AddressModes.zpgY, "LDX Z{0},Y", 2);
        public static OpCode _AE = new OpCode(0xAE, AddressModes.abs, "LDX ${0}", 3);
        public static OpCode _BE = new OpCode(0xBE, AddressModes.absY, "LDX ${0},Y", 3);

        //LDY
        public static OpCode _A0 = new OpCode(0xA0, AddressModes.imm, "LDY #{1}", 2);
        public static OpCode _A4 = new OpCode(0xA4, AddressModes.zpg, "LDY Z{0}", 2);
        public static OpCode _B4 = new OpCode(0xB4, AddressModes.zpgX, "LDY Z{0},X", 2);
        public static OpCode _AC = new OpCode(0xAC, AddressModes.abs, "LDY ${0}", 3);
        public static OpCode _BC = new OpCode(0xBC, AddressModes.absX, "LDY ${0},X", 3);

        //LSR
        public static OpCode _4A = new OpCode(0x4A, AddressModes.a, "LSR A", 1);
        public static OpCode _46 = new OpCode(0x46, AddressModes.zpg, "LSR Z{0}", 2);
        public static OpCode _56 = new OpCode(0x56, AddressModes.zpgX, "LSR Z{0},X", 2);
        public static OpCode _4E = new OpCode(0x4E, AddressModes.abs, "LSR ${0}", 3);
        public static OpCode _5E = new OpCode(0x5E, AddressModes.absX, "LSR ${0},X", 3);

        //NOP
        public static OpCode _EA = new OpCode(0xEA, AddressModes.impl, "NOP", 1);

        //ORA
        public static OpCode _09 = new OpCode(0x09, AddressModes.imm, "ORA #{1}", 2);
        public static OpCode _05 = new OpCode(0x05, AddressModes.zpg, "ORA Z{0}", 2);
        public static OpCode _15 = new OpCode(0x15, AddressModes.zpgX, "ORA Z{0},X", 2);
        public static OpCode _0D = new OpCode(0x0D, AddressModes.abs, "ORA ${0}", 3);
        public static OpCode _1D = new OpCode(0x1D, AddressModes.absX, "ORA ${0},X", 3);
        public static OpCode _19 = new OpCode(0x19, AddressModes.absY, "ORA ${0},Y", 3);
        public static OpCode _01 = new OpCode(0x01, AddressModes.Xind, "ORA (${0},X)", 2);
        public static OpCode _11 = new OpCode(0x11, AddressModes.indY, "ORA (${0}),Y", 2);

        //PHA
        public static OpCode _48 = new OpCode(0x48, AddressModes.impl, "PHA", 1);

        //PHP
        public static OpCode _08 = new OpCode(0x08, AddressModes.impl, "PHP", 1);

        //PLA
        public static OpCode _68 = new OpCode(0x68, AddressModes.impl, "PLA", 1);

        //PLP
        public static OpCode _28 = new OpCode(0x28, AddressModes.impl, "PLP", 1);

        //ROL
        public static OpCode _2A = new OpCode(0x2A, AddressModes.a, "ROL A", 1);
        public static OpCode _26 = new OpCode(0x26, AddressModes.zpg, "ROL Z{0}", 2);
        public static OpCode _36 = new OpCode(0x36, AddressModes.zpgX, "ROL Z{0},X", 2);
        public static OpCode _2E = new OpCode(0x2E, AddressModes.abs, "ROL ${0}", 3);
        public static OpCode _3E = new OpCode(0x3E, AddressModes.absX, "ROL ${0},X", 3);

        //ROR
        public static OpCode _6A = new OpCode(0x6A, AddressModes.a, "ROR A", 1);
        public static OpCode _66 = new OpCode(0x66, AddressModes.zpg, "ROR Z{0}", 2);
        public static OpCode _76 = new OpCode(0x76, AddressModes.zpgX, "ROR Z{0},X", 2);
        public static OpCode _6E = new OpCode(0x6E, AddressModes.abs, "ROR ${0}", 3);
        public static OpCode _7E = new OpCode(0x7E, AddressModes.absX, "ROR ${0},X", 3);

        //RTI
        public static OpCode _40 = new OpCode(0x40, AddressModes.impl, "RTI", 1);

        //RTS
        public static OpCode _60 = new OpCode(0x60, AddressModes.impl, "RTS", 1);

        //SBC
        public static OpCode _E9 = new OpCode(0xE9, AddressModes.imm, "SBC #{1}", 2);
        public static OpCode _E5 = new OpCode(0xE5, AddressModes.zpg, "SBC Z{0}", 2);
        public static OpCode _F5 = new OpCode(0xF5, AddressModes.zpgX, "SBC Z{0},X", 2);
        public static OpCode _ED = new OpCode(0xED, AddressModes.abs, "SBC ${0}", 3);
        public static OpCode _FD = new OpCode(0xFD, AddressModes.absX, "SBC ${0},X", 3);
        public static OpCode _F9 = new OpCode(0xF9, AddressModes.absY, "SBC ${0},Y", 3);
        public static OpCode _E1 = new OpCode(0xE1, AddressModes.Xind, "SBC (${0},X)", 2);
        public static OpCode _F1 = new OpCode(0xF1, AddressModes.indY, "SBC (${0}),Y", 2);

        //SEC
        public static OpCode _38 = new OpCode(0x38, AddressModes.impl, "SEC", 1);

        //SED
        public static OpCode _F8 = new OpCode(0xF8, AddressModes.impl, "SED", 1);

        //SEI
        public static OpCode _78 = new OpCode(0x78, AddressModes.impl, "SEI", 1);

        //STA
        public static OpCode _85 = new OpCode(0x85, AddressModes.zpg, "STA Z{0}", 2);
        public static OpCode _95 = new OpCode(0x95, AddressModes.zpgX, "STA Z{0},X", 2);
        public static OpCode _8D = new OpCode(0x8D, AddressModes.abs, "STA ${0}", 3);
        public static OpCode _9D = new OpCode(0x9D, AddressModes.absX, "STA ${0},X", 3);
        public static OpCode _99 = new OpCode(0x99, AddressModes.absY, "STA ${0},Y", 3);
        public static OpCode _81 = new OpCode(0x81, AddressModes.Xind, "STA (${0},X)", 2);
        public static OpCode _91 = new OpCode(0x91, AddressModes.indY, "STA (${0}),Y", 2);

        //STX
        public static OpCode _86 = new OpCode(0x86, AddressModes.zpg, "STX Z{0}", 2);
        public static OpCode _96 = new OpCode(0x96, AddressModes.zpgY, "STX Z{0},Y", 2);
        public static OpCode _8E = new OpCode(0x8E, AddressModes.abs, "STX ${0}", 3);

        //STY
        public static OpCode _84 = new OpCode(0x84, AddressModes.zpg, "STY Z{0}", 2);
        public static OpCode _94 = new OpCode(0x94, AddressModes.zpgX, "STY Z{0},X", 2);
        public static OpCode _8C = new OpCode(0x8C, AddressModes.abs, "STY ${0}", 3);

        //TAX
        public static OpCode _AA = new OpCode(0xAA, AddressModes.impl, "TAX", 1);

        //TAY
        public static OpCode _A8 = new OpCode(0xA8, AddressModes.impl, "TAY", 1);

        //TSX
        public static OpCode _BA = new OpCode(0xBA, AddressModes.impl, "TSX", 1);

        //TXA
        public static OpCode _8A = new OpCode(0x8A, AddressModes.impl, "TXA", 1);

        //TXS
        public static OpCode _9A = new OpCode(0x9A, AddressModes.impl, "TXS", 1);

        //TYA
        public static OpCode _98 = new OpCode(0x98, AddressModes.impl, "TYA", 1);

        public static OpCode GetOpCode(byte c)
        {
            if (OPCODES.ContainsKey(c))
            {
                return OPCODES[c];
            }
            return null;
        }

        public readonly AddressModes Addressing;
        public readonly string Assembler;
        public readonly byte bytes;

        public OpCode(byte oc, AddressModes am, string asm, byte b)
        {
            Addressing = am;
            Assembler = asm;
            bytes = b;

            if (OPCODES.ContainsKey(oc)) throw new Exception($"OpCode ({oc.ToString("X2")}) already defined");
            OPCODES.Add(oc, this);
        }

        public string ToString(ushort addr, byte value, sbyte shortValue)
        {
            return string.Format(Assembler, addr.ToHex(), value.ToHex(), shortValue);
        }

        public override string ToString()
        {
            return Assembler;
        }
    }
}
