using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GuiFasel.Classes
{
    public class Cinfiguration : INotifyPropertyChanged
    {
        private int _UpConvert;
        public int UpConvert
        {
            get { return _UpConvert; }
            set { _UpConvert = value; OnPropertyChanged("UpConvert"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private int _DownConvert;
        public int DownConvert
        {
            get { return _DownConvert; }
            set { _DownConvert = value; OnPropertyChanged("DownConvert"); }
        }

        private int _RfHead;
        public int RfHead
        {
            get { return _RfHead; }
            set { _RfHead = value; OnPropertyChanged("RF-Head"); }
        }

        private int _RXBeanFormer;
        public int RXBeanFormer
        {
            get { return _RXBeanFormer; }
            set { _RXBeanFormer = value; OnPropertyChanged("RXBeanFormer"); }
        }

        private int _TXBeanFormer;
        public int TXBeanFormer
        {
            get { return _TXBeanFormer; }
            set { _TXBeanFormer = value; OnPropertyChanged("TXBeanFormer"); }
        }

        private int _MainSwitch;
        public int MainSwitch
        {
            get { return _MainSwitch; }
            set { _MainSwitch = value; OnPropertyChanged("MainSwitch"); }
        }
    }
    public static class GetConfig
    {
        private static XmlDocument doc;
        private const string FileName = "Configuration.Xml";
        private static MemoryStream ms;

        static GetConfig()
        {
            doc = new XmlDocument { PreserveWhitespace = true };
            byte[] buffer;
            using (var fs = new FileStream(FileName, FileMode.Open))
            {

                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                fs.Close();
                ms = new MemoryStream(buffer) { Position = 0 };
            }
            ms.Position = 0;
            doc.Load(ms);
            ms = null;
        }
        private static void Save()
        {
            doc.Save(FileName);
        }
        public static void SetParam(string xPath, string strValue)
        {
            doc.SelectSingleNode("/Configuration/" + xPath).InnerText = strValue;
            Save();
        }
        public static string GetParam(string xPath)
        {
            return doc.SelectSingleNode("/Configuration/" + xPath).InnerText;
        }
    }

}
