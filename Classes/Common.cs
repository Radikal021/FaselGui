using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace GuiFasel
{
    public class Common
    {
        public static string LocalIp = "127.0.0.1";
        public static List<byte> ReceiveData = new List<byte>();
        short freamCounter = 0;
        public byte Source = 0x11;
        public byte Destenation = 0x10;
        public static object MonitorReciveData = new object();

        public void GenPacket(byte opCode, List<byte> upconvert, List<byte> downconvert, List<byte> rfHead, List<byte> RxBeanFormer, List<byte> TxBeanFormer, List<byte> MainSwitch, List<byte> SubPower)
        {
            freamCounter++;
            ReceiveData.Add(0x68); ReceiveData.Add(0x65); ReceiveData.Add(0x61); ReceiveData.Add(0x64);  //HEAD
            byte[] aData = BitConverter.GetBytes(freamCounter);
            for (int i = 0; i < aData.Length; i++) ReceiveData.Add(aData[i]);//freamCounter
            ReceiveData.Add(0x0B); //DataType
            ReceiveData.Add(Source);  // Source
            ReceiveData.Add(Destenation); //Dest
            ReceiveData.Add(opCode);     //Opcode
            int size = upconvert.Count + downconvert.Count + rfHead.Count + RxBeanFormer.Count + TxBeanFormer.Count + MainSwitch.Count + SubPower.Count; //Size
            aData = BitConverter.GetBytes(size);
            for (int i = 0; i < aData.Length; i++) ReceiveData.Add(aData[i]);
            ReceiveData.AddRange(upconvert);     //Data
            ReceiveData.AddRange(downconvert);   //Data
            ReceiveData.AddRange(rfHead);        //Data
            ReceiveData.AddRange(RxBeanFormer);  //Data
            ReceiveData.AddRange(TxBeanFormer);  //Data
            ReceiveData.AddRange(MainSwitch);    //Data
            ReceiveData.AddRange(SubPower);      //Data
            //upconvert.ToArray();
            ReceiveData.Add(0xAA); //Footer
            ReceiveData.Add(0xBB); //Footer
            ReceiveData.Add(0xCC); //Footer
            ReceiveData.Add(0xDD); //Footer
            ReceiveData.ToArray();
        }
        public class UpConvert
        {
            public byte AbsentPrresent = 1;
            public byte SystenmMode = 1;
            public short RFAttenutor = 2;
            public uint PLLFreq = 4;
            public byte PLLlock = 1;
            public byte Opcode = 1;
            public uint Value = 4;
        }
        public class DownConvert
        {
            public byte AbsentPrresent = 1;
            public byte SystenmMode = 1;
            public short RFAttenuator = 2;
            public uint PLLFreq = 4;
            public byte PLLLock = 1;
            public byte OpCode = 1;
            public uint Value = 4;
        }
        public class RFHead
        {
            public byte AbsentPrresent = 1;
            public byte SystenmMode = 1;
            public short RFAttenuator = 2;
            public uint PLLFreq = 4;
            public byte LNA_Freq = 1;
            public byte LNA_En = 1;
            public byte PLL_En = 1;
            public byte ControlBits = 1;
            public byte OpCode = 1;
            public uint Value = 4;
        }
        public class RXBeamFormer
        {
            public byte AbsentPrresent = 1;
            public byte SystenmMode = 1;
            public byte Switches = 1;
            public byte OperationMode = 1;
            public byte Beam = 1;
            public byte OpCode = 1;
            public uint Value = 4;
        }
        public class TXBeamFormer
        {
            public byte AbsentPrresent = 1;
            public byte SystenmMode = 1;
            public byte Switches = 1;
            public byte OperationMode = 1;
            public byte Beam = 1;
            public byte PrimarySwitch = 1;
            public byte OpCode = 1;
            public uint Value = 4;
        }
        public class MainSwitch
        {
            public byte AbsentPrresent = 1;
            public byte Switches_Presence = 1;
            public byte Subs_Presence = 1;
            public byte TRSwitch = 1;
            public byte Power = 1;
            public short Min_Temp = 2;
            public short Min_VSWR = 2;
            public short Max_Temp = 2;
            public short Max_VSWR = 2;
            public byte OpCode = 1;
            public uint Value = 4;
        }

        public class PowerAmp
        {
            public byte AbsentPrresent = 1;
            public short Current_of_50V = 2;
            public short Current_of_28V = 2;
            public short Current_of_5V = 2;
            public short Temperature = 2;
            public short PrimarySwitch = 2;
            public short Output_Level = 2;
            public short VSWR = 2;
            public short Faults = 2;
            public byte OpCode = 1;
            public short Value = 2;
        }
    }
}
