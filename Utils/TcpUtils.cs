using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace SS_Rust_Win_Gui.Utils
{
    internal class TcpUtils
    {

        public async static Task<string> TestDelayAsync(string host, int port)
        {

            // 创建 TCP 客户端
            TcpClient client = new TcpClient();
            // 连接服务器
            client.Connect(host, port);
            // 发送[测试数据]
            byte[] data = new byte[1024];
            new Random().NextBytes(data);
            Stopwatch stopwatch = Stopwatch.StartNew();


            await Task.Run(() =>
            {
                client.GetStream().Write(data, 0, data.Length);
                // 接收[测试数据]
                byte[] buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, buffer.Length);
            });



            // 计算延时
            stopwatch.Stop();
            TimeSpan delay = stopwatch.Elapsed;
            // 关闭连接
            client.Close();

            return delay.TotalMilliseconds.ToString();
        }


        public static string TestDelay(string host, int port)
        {

            // 创建 TCP 客户端
            TcpClient client = new TcpClient();
            // 连接服务器
            client.Connect(host, port);
            // 发送[测试数据]
            byte[] data = new byte[8];
            new Random().NextBytes(data);
            Stopwatch stopwatch = Stopwatch.StartNew();

            client.GetStream().Write(data, 0, data.Length);
            // 接收[测试数据]
            byte[] buffer = new byte[1024];
            client.GetStream().Read(buffer, 0, buffer.Length);

            // 计算延时
            stopwatch.Stop();
            TimeSpan delay = stopwatch.Elapsed;
            // 关闭连接
            client.Close();

            return delay.TotalMilliseconds.ToString();
        }

    }
}

