using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static GuiFasel.Common;

namespace GuiFasel.Classes
{

    public class Engine : IDisposable
    {
        public event EventHandler<EventClass> myEvent;
        ConfigUdp classUDP = new ConfigUdp();
        Thread thReciveData;
        bool flagReciveData;
        public DownConvert Down_Convert = new DownConvert();
        public UpConvert Up_Convert = new UpConvert();
        public RFHead rFHead = new RFHead();
        public RXBeamFormer rXBeamFormer = new RXBeamFormer();
        public TXBeamFormer tXBeamFormer = new TXBeamFormer();
        public MainSwitch MainSwitch = new MainSwitch();
        public PowerAmp power = new PowerAmp();

        public Engine()
        {
            thReciveData = new Thread(new ThreadStart(funThReciveData));
            flagReciveData = true;
            thReciveData.Start();
        }

        public void Dispose()
        {
            flagReciveData = false;
        }

        private void funThReciveData()
        {
            while (flagReciveData)
            {
                Monitor.Enter(Common.MonitorReciveData);

                if (Common.ReceiveData.Count > 737)
                {
                    int ind = 0;
                    while (true)
                    {
                        if (Common.ReceiveData[ind] == 0x68 && Common.ReceiveData[ind] == 0x68 &&
                            Common.ReceiveData[ind] == 0x68 && Common.ReceiveData[ind] == 0x68)
                        {
                            ind += 4;
                            int fc = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind += 2;
                            byte dataType = Common.ReceiveData[ind]; ind++;
                            ind++;
                            ind++;
                            byte opCode = 1; ind++;
                            ind += 4; //Size

                            #region down
                            //Down_Convert
                            Down_Convert.AbsentPrresent = Common.ReceiveData[ind]; ind++;
                            Down_Convert.SystenmMode = Common.ReceiveData[ind]; ind++;
                            Down_Convert.RFAttenuator = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind += 2;
                            Down_Convert.PLLFreq = BitConverter.ToUInt32(Common.ReceiveData.ToArray(), ind); ind += 4;
                            Down_Convert.PLLLock = Common.ReceiveData[ind]; ind++;
                            Down_Convert.OpCode = Common.ReceiveData[ind]; ind++;
                            Down_Convert.Value = BitConverter.ToUInt32(Common.ReceiveData.ToArray(), ind); ind += 4;


                            //Up_Convert
                            Up_Convert.AbsentPrresent = Common.ReceiveData[ind]; ind++;
                            Up_Convert.SystenmMode = Common.ReceiveData[ind]; ind++;
                            Up_Convert.RFAttenutor = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind += 2;
                            Up_Convert.PLLFreq = BitConverter.ToUInt32(Common.ReceiveData.ToArray(), ind); ind += 4;
                            Up_Convert.PLLlock = Common.ReceiveData[ind]; ind++;
                            Up_Convert.Opcode = Common.ReceiveData[ind]; ind++;
                            Up_Convert.Value = BitConverter.ToUInt32(Common.ReceiveData.ToArray(), ind); ind += 4;

                            //RfHead
                            rFHead.AbsentPrresent = Common.ReceiveData[ind]; ind++;
                            rFHead.SystenmMode = Common.ReceiveData[ind]; ind++;
                            rFHead.RFAttenuator = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind += 2;
                            rFHead.PLLFreq = BitConverter.ToUInt32(Common.ReceiveData.ToArray(), ind); ind += 4;
                            rFHead.LNA_En = Common.ReceiveData[ind]; ind++;
                            rFHead.LNA_En = Common.ReceiveData[ind]; ind++;
                            rFHead.ControlBits = Common.ReceiveData[ind]; ind++;
                            rFHead.OpCode = Common.ReceiveData[ind]; ind++;
                            rFHead.Value = BitConverter.ToUInt32(Common.ReceiveData.ToArray(), ind);

                            //rXBeamFormer 
                            rXBeamFormer.AbsentPrresent = Common.ReceiveData[ind]; ind++;
                            rXBeamFormer.SystenmMode = Common.ReceiveData[ind]; ind++;
                            rXBeamFormer.Switches = Common.ReceiveData[ind]; ind++;
                            rXBeamFormer.OperationMode = Common.ReceiveData[ind]; ind++;
                            rXBeamFormer.Beam = Common.ReceiveData[ind]; ind++;
                            rXBeamFormer.OpCode = Common.ReceiveData[ind]; ind++;
                            rXBeamFormer.Value = BitConverter.ToUInt32(Common.ReceiveData.ToArray(), ind);

                            //tXBeamFormer
                            tXBeamFormer.AbsentPrresent = Common.ReceiveData[ind]; ind++;
                            tXBeamFormer.SystenmMode = Common.ReceiveData[ind]; ind++;
                            tXBeamFormer.Switches = Common.ReceiveData[ind]; ind++;
                            tXBeamFormer.OperationMode = Common.ReceiveData[ind]; ind++;
                            tXBeamFormer.Beam = Common.ReceiveData[ind]; ind++;
                            tXBeamFormer.PrimarySwitch = Common.ReceiveData[ind]; ind++;
                            tXBeamFormer.OpCode = Common.ReceiveData[ind]; ind++;
                            tXBeamFormer.Value = BitConverter.ToUInt32(Common.ReceiveData.ToArray(), ind);

                            //MainSwitch
                            MainSwitch.AbsentPrresent = Common.ReceiveData[ind]; ind++;
                            MainSwitch.Switches_Presence = Common.ReceiveData[ind]; ind++;
                            MainSwitch.Subs_Presence = Common.ReceiveData[ind]; ind++;
                            MainSwitch.TRSwitch = Common.ReceiveData[ind]; ind++;
                            MainSwitch.Power = Common.ReceiveData[ind]; ind++;
                            MainSwitch.Min_Temp = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind++;
                            MainSwitch.Min_VSWR = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind++;
                            MainSwitch.Max_Temp = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind++;
                            MainSwitch.Max_VSWR = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind++;
                            MainSwitch.OpCode = Common.ReceiveData[ind]; ind++;
                            MainSwitch.Value = BitConverter.ToUInt32(Common.ReceiveData.ToArray(), ind);

                            //Power
                            power.AbsentPrresent = Common.ReceiveData[ind]; ind++;
                            power.Current_of_50V = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind);
                            power.Current_of_28V = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind);
                            power.Current_of_5V = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind);
                            power.Temperature = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind);
                            power.PrimarySwitch = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind++;
                            power.Output_Level = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind++;
                            power.VSWR = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind++;
                            power.Faults = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind++;
                            power.OpCode = Common.ReceiveData[ind]; ind++;
                            power.Value = BitConverter.ToInt16(Common.ReceiveData.ToArray(), ind); ind += 4;
                            #endregion

                            myEvent(this, new EventClass(0, opCode));
                            break;
                        }
                        else
                        {
                            ind++;
                        }
                    }
                    ReceiveData.RemoveRange(0, 737);
                }
                Monitor.Exit(Common.MonitorReciveData);
                Thread.Sleep(500);
            }
        }
    }
}
