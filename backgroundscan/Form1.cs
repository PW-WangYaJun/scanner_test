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
using HidLibrary;
using System.IO.Ports;


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

            SerialPort serialPort1 = new SerialPort();
            SerialPort serialPort2 = new SerialPort();
            serialPort1.PortName = "COM3";  // 设置串口名称，例如 COM3 (Windows) 或 /dev/ttyUSB0 (Linux)
            serialPort1.BaudRate = 9600;    // 设置波特率
            serialPort1.Parity = Parity.None;  // 校验位
            serialPort1.DataBits = 8;       // 数据位
            serialPort1.StopBits = StopBits.One; // 停止位
            serialPort1.Handshake = Handshake.None; // 流控制
            
            // 添加 DataReceived 事件处理程序
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler1);

            // 打开串口
            serialPort1.Open();






            serialPort2.PortName = "COM4";  // 设置串口名称，例如 COM3 (Windows) 或 /dev/ttyUSB0 (Linux)
            serialPort2.BaudRate = 9600;    // 设置波特率
            serialPort2.Parity = Parity.None;  // 校验位
            serialPort2.DataBits = 8;       // 数据位
            serialPort2.StopBits = StopBits.One; // 停止位
            serialPort2.Handshake = Handshake.None; // 流控制

            // 添加 DataReceived 事件处理程序
            serialPort2.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler2);

            // 打开串口
            serialPort2.Open();

        }

        // 处理接收到的数据
        private static void DataReceivedHandler1(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();  // 读取串口中的所有数据
            MessageBox.Show($"scanner1: {inData}");
        }
        private static void DataReceivedHandler2(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();  // 读取串口中的所有数据
            MessageBox.Show($"scanner2: {inData}");
        }
    }
}
