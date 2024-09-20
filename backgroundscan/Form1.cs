using LibUsbDotNet.Main;
using LibUsbDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Newtonsoft.Json;


namespace backgroundscan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var vid = Convert.ToInt16("0x05E0", 16);
            var pid= Convert.ToInt16("0x1200", 16);
            UsbDeviceFinder usbFinder = new UsbDeviceFinder(vid, pid);
            UsbDevice usbDevice = UsbDevice.OpenUsbDevice(usbFinder);
            if (usbDevice == null)
            {
                MessageBox.Show("Device not found");
            }
            else 
            {
                MessageBox.Show("Device found");
            }
            
            IUsbDevice wholeUsbDevice = usbDevice as IUsbDevice;
            if (!ReferenceEquals(wholeUsbDevice, null))
            {
                // 选择配置并声明接口
                wholeUsbDevice.SetConfiguration(1);
                wholeUsbDevice.ClaimInterface(0);  // Interface 0，可能需要根据设备实际配置更改
            }
            UsbEndpointReader reader = usbDevice.OpenEndpointReader(ReadEndpointID.Ep01);

            byte[] readBuffer = new byte[256]; // 根据设备数据包大小定义
            ErrorCode ec = reader.Read(readBuffer,1000000, out int bytesRead);

            if (ec == ErrorCode.None && bytesRead > 0)
            {

                string barcodeData = Convert.ToString(readBuffer);
                MessageBox.Show($"Barcode Data: {barcodeData}");
            }
            else
            {
                MessageBox.Show("Error reading data: " + ec);
            }
            usbDevice.Close();
            UsbDevice.Exit();
        }
    }
}
