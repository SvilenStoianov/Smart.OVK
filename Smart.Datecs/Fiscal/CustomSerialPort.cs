using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Smart.Datecs.Fiscal
{
    public class TcpSerialPort : IDisposable
    {
        private TcpClient Tcp;
        public string HostUrl { get; set; }
        public int HostPort { get; set; }

        private const byte EndOfText = 3;
        private const byte NAK = 21;
        private const byte SYN = 22;

        private byte[] LastCommand;

        public Action<string> OnErrorLog { get; set; }
        public Action<string> OnDebugLog { get; set; }

        public bool IsOpen
        {
            get
            {
                return (Tcp != null && Tcp.Connected);
            }
        }

        public TcpSerialPort(string hostUrl, int hostPort)
        {
            this.Tcp = new TcpClient();
            this.Tcp.LingerState = new LingerOption(true, 0);
            this.Tcp.NoDelay = true;
            this.Tcp.ReceiveTimeout = 10 * 1000;
            this.Tcp.SendTimeout = 10 * 1000;
            this.HostUrl = hostUrl;
            this.HostPort = hostPort;

            this.LastCommand = new byte[0];
        }

        public bool Open()
        {
            if (IsOpen)
            {
                Close();
            }

            Tcp.Connect(this.HostUrl, this.HostPort);
            return this.IsOpen;
        }

        public void Close()
        {
            if (Tcp != null)
            {
                Tcp.Client.Disconnect(true);
            }
        }

        public CommandReply ReadCommand()
        {
            var arr = new List<byte>();

            int retry = 0;
            var stream = Tcp.GetStream();
            while (true)
            {
                if (Tcp.Available > 0)
                {
                    retry = 0;
                    byte[] buf = new byte[Tcp.Available];
                    stream.Read(buf, 0, buf.Length);
                    arr.AddRange(buf);

                    this.OnDebugLog?.Invoke(string.Format("DataReceived Buffer: CMD: {0} BUF: {1}",
                        Encoding.UTF8.GetString(this.LastCommand),
                        Encoding.UTF8.GetString(buf)));
                }

                if (arr.Contains(EndOfText))
                {
                    this.OnDebugLog?.Invoke("Enqueue message");
                    arr.RemoveAll(p => p == NAK || p == SYN);
                    return new CommandReply(arr.ToArray());
                }
                else if (arr.Contains(NAK) || retry == 20)
                {
                    this.OnErrorLog?.Invoke("Изпрати NAK!");
                    this.Write(this.LastCommand, 0, this.LastCommand.Length);
                    arr.Clear();
                }

                if (retry >= 200)
                {
                    this.OnErrorLog?.Invoke("Изтече тиме оут! Buffer: " + System.Text.Encoding.GetEncoding(1251).GetString(arr.ToArray()));
                    return CommandReply.Empty;
                }

                retry++;
                Thread.Sleep(60);
            }
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            this.LastCommand = buffer;

            this.Tcp.GetStream().Write(buffer, offset, count);
        }

        public void Dispose()
        {
            this.Close();
        }
    }
}
