using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using GuiFasel.Classes;

namespace GuiFasel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Engine myEngine;
        UdpClient UdpClient = new UdpClient();
        ConfigUdp configUdp = new ConfigUdp();
        public MainWindow()
        {
            InitializeComponent();
            myEngine = new Engine();
            myEngine.myEvent += MyEngine_myEvent1;
        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            UdpClient.Send(Common.ReceiveData.ToArray(), Common.ReceiveData.Count);
            configUdp.GetStart();
        }

        private void MyEngine_myEvent1(object sender, EventClass e)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                switch (e.Opcod)
                {
                    case 1:
                        //DownConvert
                        lblDownConvert1.Text = myEngine.Down_Convert.AbsentPrresent.ToString();
                        lblDownConvert2.Text = myEngine.Down_Convert.SystenmMode.ToString();
                        lblDownConvert3.Text = myEngine.Down_Convert.RFAttenuator.ToString();
                        lblDownConvert4.Text = myEngine.Down_Convert.PLLFreq.ToString();
                        lblDownConvert4.Text = myEngine.Down_Convert.PLLLock.ToString();
                        lblDownConvert5.Text = myEngine.Down_Convert.OpCode.ToString();
                        lblDownConvert6.Text = myEngine.Down_Convert.Value.ToString();

                        //Upconvert
                        lbl1UpConvert1.Text = myEngine.Up_Convert.AbsentPrresent.ToString();
                        lbl1UpConvert2.Text = myEngine.Up_Convert.SystenmMode.ToString();
                        lbl1UpConvert3.Text = myEngine.Up_Convert.RFAttenutor.ToString();
                        lbl1UpConvert4.Text = myEngine.Up_Convert.PLLFreq.ToString();
                        lbl1UpConvert5.Text = myEngine.Up_Convert.PLLlock.ToString();
                        lbl1UpConvert6.Text = myEngine.Up_Convert.Opcode.ToString();
                        lbl1UpConvert7.Text = myEngine.Up_Convert.Value.ToString();

                        //RfHead
                        lblRFHead1.Text = myEngine.rFHead.AbsentPrresent.ToString();
                        lblRFHead2.Text = myEngine.rFHead.SystenmMode.ToString();
                        lblRFHead3.Text = myEngine.rFHead.RFAttenuator.ToString();
                        lblRFHead4.Text = myEngine.rFHead.PLLFreq.ToString();
                        lblRFHead5.Text = myEngine.rFHead.LNA_En.ToString();
                        lblRFHead5.Text = myEngine.rFHead.PLL_En.ToString();
                        lblRFHead5.Text = myEngine.rFHead.ControlBits.ToString();
                        lblRFHead6.Text = myEngine.rFHead.OpCode.ToString();
                        lblRFHead7.Text = myEngine.rFHead.Value.ToString();

                        //RXBeanFormer
                        lblRX1.Text = myEngine.rXBeamFormer.AbsentPrresent.ToString();
                        lblRX2.Text = myEngine.rXBeamFormer.SystenmMode.ToString();
                        lblRX3.Text = myEngine.rXBeamFormer.Switches.ToString();
                        lblRX4.Text = myEngine.rXBeamFormer.OperationMode.ToString();
                        lblRX5.Text = myEngine.rXBeamFormer.Beam.ToString();
                        lblRX6.Text = myEngine.rXBeamFormer.OpCode.ToString();
                        lblRX7.Text = myEngine.rXBeamFormer.Value.ToString();

                        //TXBeanFormer
                        lblDownConvert1.Text = myEngine.tXBeamFormer.AbsentPrresent.ToString();
                        lblDownConvert2.Text = myEngine.tXBeamFormer.SystenmMode.ToString();
                        lblDownConvert3.Text = myEngine.tXBeamFormer.Switches.ToString();
                        lblDownConvert4.Text = myEngine.tXBeamFormer.OperationMode.ToString();
                        lblDownConvert4.Text = myEngine.tXBeamFormer.Beam.ToString();
                        lblDownConvert5.Text = myEngine.tXBeamFormer.PrimarySwitch.ToString();
                        lblDownConvert5.Text = myEngine.tXBeamFormer.OpCode.ToString();
                        lblDownConvert6.Text = myEngine.tXBeamFormer.Value.ToString();

                        //MainSwitch
                        lblSwitch1.Text = myEngine.MainSwitch.AbsentPrresent.ToString();
                        lblSwitch2.Text = myEngine.MainSwitch.Switches_Presence.ToString();
                        lblSwitch3.Text = myEngine.MainSwitch.Subs_Presence.ToString();
                        lblSwitch4.Text = myEngine.MainSwitch.TRSwitch.ToString();
                        lblSwitch5.Text = myEngine.MainSwitch.Power.ToString();
                        lblSwitch6.Text = myEngine.MainSwitch.Min_Temp.ToString();
                        lblSwitch7.Text = myEngine.MainSwitch.Min_VSWR.ToString();
                        lblSwitch7.Text = myEngine.MainSwitch.Max_Temp.ToString();
                        lblSwitch7.Text = myEngine.MainSwitch.Max_VSWR.ToString();
                        lblSwitch7.Text = myEngine.MainSwitch.OpCode.ToString();
                        lblSwitch8.Text = myEngine.MainSwitch.Value.ToString();

                        //Power
                        lblpower1.Text = myEngine.power.AbsentPrresent.ToString();
                        lblpower2.Text = myEngine.power.Current_of_50V.ToString();
                        lblpower3.Text = myEngine.power.Current_of_28V.ToString();
                        lblpower4.Text = myEngine.power.Current_of_5V.ToString();
                        lblpower5.Text = myEngine.power.Temperature.ToString();
                        lblpower6.Text = myEngine.power.PrimarySwitch.ToString();
                        lblpower7.Text = myEngine.power.Output_Level.ToString();
                        lblpower8.Text = myEngine.power.VSWR.ToString();
                        lblpower9.Text = myEngine.power.Faults.ToString();
                        lblpower10.Text = myEngine.power.OpCode.ToString();
                        lblpower11.Text = myEngine.power.Value.ToString();
                        break;
                }
            });
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //UPConvert
            string lengthUpconvert = GetConfig.GetParam("Upconvert");
            string[] leArray = lengthUpconvert.Split(new string[] { ",", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);

            lbl1UpConvert1.Text = leArray[0];
            lbl1UpConvert2.Text = leArray[1];
            lbl1UpConvert3.Text = leArray[2];
            lbl1UpConvert4.Text = leArray[3];
            lbl1UpConvert5.Text = leArray[4];
            lbl1UpConvert6.Text = leArray[5];
            lbl1UpConvert7.Text = leArray[6];
            //lbl1UpConvert8.Text = leArray[7];
            //lbl1UpConvert9.Text = leArray[8];
            //lbl1UpConvert10.Text = leArray[9];
            //lbl1UpConvert11.Text = leArray[10];
            //lbl1UpConvert12.Text = leArray[11];
            //lbl1UpConvert13.Text = leArray[12];
            //lbl1UpConvert14.Text = leArray[13];
            //lbl1UpConvert15.Text = leArray[14];
            //lbl1UpConvert16.Text = leArray[15];
            //lbl1UpConvert17.Text = leArray[16];
            //lbl1UpConvert18.Text = leArray[17];
            //lbl1UpConvert19.Text = leArray[18];
            //lbl1UpConvert20.Text = leArray[19];


            //DownConvert
            string lengthDownconvert = GetConfig.GetParam("Downconvert");
            string[] leDownconvert = lengthDownconvert.Split(new string[] { ",", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            lblDownConvert2.Text = leDownconvert[1];
            lblDownConvert1.Text = leDownconvert[0];
            lblDownConvert3.Text = leDownconvert[2];
            lblDownConvert4.Text = leDownconvert[3];
            lblDownConvert5.Text = leDownconvert[4];
            lblDownConvert6.Text = leDownconvert[5];
            lblDownConvert7.Text = leDownconvert[6];
            //lblDownConvert8.Text = leDownconvert[7];
            //lblDownConvert9.Text = leDownconvert[8];
            //lblDownConvert10.Text = leDownconvert[9];
            //lblDownConvert11.Text = leDownconvert[10];
            //lblDownConvert12.Text = leDownconvert[11];
            //lblDownConvert13.Text = leDownconvert[12];
            //lblDownConvert14.Text = leDownconvert[13];
            //lblDownConvert15.Text = leDownconvert[14];
            //lblDownConvert16.Text = leDownconvert[15];
            //lblDownConvert17.Text = leDownconvert[16];
            //lblDownConvert18.Text = leDownconvert[17];
            //lblDownConvert19.Text = leDownconvert[18];
            //lblDownConvert20.Text = leDownconvert[19];

            //RF-Head
            string lengthRFHead = GetConfig.GetParam("RF-Head");
            string[] leRFHead = lengthRFHead.Split(new string[] { ",", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            lblRFHead1.Text = leRFHead[0];
            lblRFHead2.Text = leRFHead[1];
            lblRFHead3.Text = leRFHead[2];
            lblRFHead4.Text = leRFHead[3];
            lblRFHead5.Text = leRFHead[4];
            lblRFHead6.Text = leRFHead[5];
            lblRFHead7.Text = leRFHead[6];
            //lblRFHead8.Text = leRFHead[7];
            //lblRFHead9.Text = leRFHead[8];
            //lblRFHead10.Text = leRFHead[9];
            //lblRFHead11.Text = leRFHead[10];
            //lblRFHead12.Text = leRFHead[11];
            //lblRFHead13.Text = leRFHead[12];
            //lblRFHead14.Text = leRFHead[13];
            //lblRFHead15.Text = leRFHead[14];
            //lblRFHead16.Text = leRFHead[15];
            //lblRFHead17.Text = leRFHead[16];
            //lblRFHead18.Text = leRFHead[17];
            //lblRFHead19.Text = leRFHead[18];
            //lblRFHead20.Text = leRFHead[19];

            //Rx - BeamFormer
            string lengthBeamFormer = GetConfig.GetParam("Rx-BeamFormer");
            string[] leRxBeamFormer = lengthBeamFormer.Split(new string[] { ",", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            lblRX1.Text = leRxBeamFormer[0];
            lblRX2.Text = leRxBeamFormer[1];
            lblRX3.Text = leRxBeamFormer[2];
            lblRX4.Text = leRxBeamFormer[3];
            lblRX5.Text = leRxBeamFormer[4];
            lblRX6.Text = leRxBeamFormer[5];
            lblRX7.Text = leRxBeamFormer[6];
            //lblRX8.Text = leRxBeamFormer[7];
            //lblRX9.Text = leRxBeamFormer[8];
            //lblRX10.Text = leRxBeamFormer[9];
            //lblRX11.Text = leRxBeamFormer[10];
            //lblRX12.Text = leRxBeamFormer[11];
            //lblRX13.Text = leRxBeamFormer[12];
            //lblRX14.Text = leRxBeamFormer[13];
            //lblRX15.Text = leRxBeamFormer[14];
            //lblRX16.Text = leRxBeamFormer[15];
            //lblRX17.Text = leRxBeamFormer[16];
            //lblRX18.Text = leRxBeamFormer[17];
            //lblRX19.Text = leRxBeamFormer[18];
            //lblRX20.Text = leRxBeamFormer[19];

            //Tx - BeamFormer
            string lengthTxBeamFormer = GetConfig.GetParam("Tx-BeamFormer");
            string[] leTxBeamFormer = lengthTxBeamFormer.Split(new string[] { ",", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            lblTX1.Text = leTxBeamFormer[0];
            lblTX2.Text = leTxBeamFormer[1];
            lblTX3.Text = leTxBeamFormer[2];
            lblTX4.Text = leTxBeamFormer[3];
            lblTX5.Text = leTxBeamFormer[4];
            lblTX6.Text = leTxBeamFormer[5];
            lblTX7.Text = leTxBeamFormer[6];
            //lblTX8.Text = leTxBeamFormer[7];
            //lblTX9.Text = leTxBeamFormer[8];
            //lblTX10.Text = leTxBeamFormer[9];
            //lblTX11.Text = leTxBeamFormer[10];
            //lblTX12.Text = leTxBeamFormer[11];
            //lblTX13.Text = leTxBeamFormer[12];
            //lblTX14.Text = leTxBeamFormer[13];
            //lblTX15.Text = leTxBeamFormer[14];
            //lblTX16.Text = leTxBeamFormer[15];
            //lblTX17.Text = leTxBeamFormer[16];
            //lblTX18.Text = leTxBeamFormer[17];
            //lblTX19.Text = leTxBeamFormer[18];
            //lblTX20.Text = leTxBeamFormer[19];

            string lengthMainSwitch = GetConfig.GetParam("MainSwitch");
            string[] leMainSwitch = lengthBeamFormer.Split(new string[] { ",", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            lblSwitch1.Text = leMainSwitch[0];
            lblSwitch2.Text = leMainSwitch[2];
            lblSwitch3.Text = leMainSwitch[3];
            lblSwitch4.Text = leMainSwitch[1];
            lblSwitch5.Text = leMainSwitch[4];
            //lblSwitch6.Text = leMainSwitch[5];
            //lblSwitch7.Text = leMainSwitch[6];
            //lblSwitch8.Text = leMainSwitch[7];
            //lblSwitch9.Text = leMainSwitch[8];
            //lblSwitch10.Text = leMainSwitch[9];
            //lblSwitch11.Text = leMainSwitch[10];
            //lblSwitch12.Text = leMainSwitch[11];
            //lblSwitch13.Text = leMainSwitch[12];
            //lblSwitch14.Text = leMainSwitch[13];
            //lblSwitch15.Text = leMainSwitch[14];
            //lblSwitch16.Text = leMainSwitch[15];
            //lblSwitch17.Text = leMainSwitch[16];
            //lblSwitch18.Text = leMainSwitch[17];
            //lblSwitch19.Text = leMainSwitch[18];
            //lblSwitch20.Text = leMainSwitch[19];

            //Power1
            string lengthPower1 = GetConfig.GetParam("Power1");
            string[] lePower1 = lengthPower1.Split(new string[] { ",", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            lblpower1.Text = lePower1[0];
            lblpower2.Text = lePower1[1];
            lblpower3.Text = lePower1[2];
            lblpower4.Text = lePower1[3];
            lblpower5.Text = lePower1[4];
            lblpower6.Text = lePower1[5];
            //lblpower7.Text = lePower1[6];
            //lblpower8.Text = lePower1[7];
            //lblpower9.Text = lePower1[8];
            //lblpower10.Text = lePower1[9];
            //lblpower11.Text = lePower1[10];
            //lblpower12.Text = lePower1[11];
            //lblpower13.Text = lePower1[12];
            //lblpower14.Text = lePower1[13];
            //lblpower15.Text = lePower1[14];
            //lblpower16.Text = lePower1[15];
            //lblpower17.Text = lePower1[16];
            //lblpower18.Text = lePower1[17];
            //lblpower19.Text = lePower1[18];
            //lblpower20.Text = lePower1[19];
        }
    }
}