//------------------------------------------------------------------------------
// Copyright (c) 2018-2019 Beijing Bytedance Technology Co., Ltd.
// All Right Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
//------------------------------------------------------------------------------

using Perfect.Marshal;

namespace Perfect.IO
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    
    using UnityEngine;

    public sealed class Connection
    {
        private enum State
        {
            NOT_CONNECT,
            CONNECTING,
            CONNECTED,
            CLOSED,
        }

        public const int CLOSE_ACTIVE = 0; // 主动断开
        public const int CLOSE_CONNECT = 1; // 连接时断开
        public const int CLOSE_RECV = 2; // 接收失败
        public const int CLOSE_SEND = 3; // 发送失败

        private Socket socket;

        private readonly Octets inStream = new Octets();
        private readonly Octets outStream = new Octets();

        private readonly Octets.Msg message = new Octets.Msg();
        private static Octets temp = new Octets();

        private Action<Connection> onConnected;       
        private Action<Connection, int> onClose;
        private Action<Connection, Octets> onRecv;

        private volatile State state;

        public void Destroy()
        {
            onConnected = null;
            onClose = null;
            onRecv = null;
        }

        public static Connection ConnectAsync(
            string host,
            int port,
            Action<Connection> onConnected,
            Action<Connection, Octets> onRecv,
            Action<Connection, int> onClose)
        {
            var newConnection = new Connection();
            newConnection.onConnected = onConnected;
            newConnection.onRecv = onRecv;
            newConnection.onClose = onClose;
            newConnection.message.maxSize = 512 * 1024;

            if (!IPAddress.TryParse(host, out var address))
            {
                Dns.BeginGetHostAddresses(host,
                    ar =>
                    {
                        var addresses = Dns.EndGetHostAddresses(ar);
                        Scheduler.PostTask(() =>
                        {
                            if (addresses != null && addresses.Length > 0)
                            {
                                System.Random random  = new System.Random();
                                address = addresses[random.Next(0, addresses.Length)];
                                newConnection.Connect(address, port);
                            }
                            else
                            {
                                Debug.LogError("can not get ipaddress by host");
                            }
                        });
                    }, null);
            }
            else
            {
                newConnection.Connect(address, port);
            }

            return newConnection;
        }

        public void Connect(IPAddress address, int port)
        {
            if (this.state != State.NOT_CONNECT)
            {
                //Debug.Log("Connection state error return,this.state != State.NOT_CONNECT, state= " + this.state);
                return;
            }
            //Debug.Log("Connection start try Connect");
            this.socket = new Socket(
                address.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            {
                SendBufferSize = 16 * 1024,
                ReceiveBufferSize = 64 * 1024,
                NoDelay = true,
            };

            this.socket.SetSocketOption(
                SocketOptionLevel.Socket, SocketOptionName.KeepAlive, false);

            this.state = State.CONNECTING;
            try
            {
                this.socket.BeginConnect(address, port, ar =>
                {
                    try
                    {
                        this.socket.EndConnect(ar);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }

                    if (this.socket.Connected)
                    {
                        this.state = State.CONNECTED;
                        Scheduler.PostTask(() =>
                        {
                            this.BeginRead();
                            //Debug.Log("Connection start connect success, trigger onConnected callBack");
                            this.onConnected?.Invoke(this);
                        });
                    }
                    else
                    {
                        this.Close(CLOSE_CONNECT);
                    }
                }, null);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                this.Close(CLOSE_CONNECT);
            }
        }

        public void Close(int errCode)
        {
            //Debug.Log("Connection close, errCode= " + errCode);
            if (this.state == State.CLOSED || this.state == State.NOT_CONNECT)
            {
                //Debug.Log("Connection close failed, state= " + this.state);
                return;
            }
            this.state = State.CLOSED;
            var disposedSocket = socket;
            try
            {
                Scheduler.PostTask(() => {
                    disposedSocket.Close();
                });
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                //Debug.Log("Connection closed, trigger onClose callBack, errCode= " + errCode);
                Scheduler.PostTask(() =>
                {
                    this.onClose?.Invoke(this, errCode);
                });
            }
        }

        public void Send(object msg)
        {
            if (this.state != State.CONNECTED)
            {
                return;
            }

            var needBeginWrite = this.outStream.Remaining == 0;
            this.outStream.MarshalOctets((Octets)msg);
            if (needBeginWrite)
            {
                this.BeginWrite();
            }
        }

        private void BeginRead()
        {
            this.inStream.SureWrite(1024);
            this.socket.BeginReceive(
                this.inStream.Bytes,
                this.inStream.WriteIndex,
                this.inStream.NotCompactWritable,
                SocketFlags.None,
                ar =>
                {
                    try
                    {
                        if (this.state == State.CONNECTED)
                        {
                            int n=0;

                            try
                            {
                                n = this.socket.EndReceive(ar);
                            }
                            catch (Exception e)
                            {
                                //
                                Debug.Log(e.Message);
                            }

                            if (n > 0)
                            {
                                Scheduler.PostTask(() =>
                                {
                                    this.inStream.AddWriteIndex(n);
                                    this.Read();
                                    this.BeginRead();
                                });
                            }
                            else
                            {
                                this.Close(CLOSE_RECV);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        this.Close(CLOSE_RECV);
                    }
                },
                null);

        }

        private void Read()
        {
            try
            {
                if (this.inStream.Remaining == 0)
                {
                    return;
                }

                while (this.inStream.TryUnmarshalBytes(this.message) == Octets.UnmarshalError.OK)
                {
                    try
                    {
                        this.onRecv?.Invoke(this, this.message.body);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                this.Close(CLOSE_ACTIVE);
                return;
            }
        }

        private void BeginWrite()
        {
            try
            {
                this.outStream.CompactBuffer();
                this.socket.BeginSend(
                    this.outStream.Bytes,
                    this.outStream.ReadIndex,
                    this.outStream.Remaining,
                    SocketFlags.None,
                    ar =>
                    {
                        try
                        {
                            var n = socket.EndSend(ar);
                            if (n > 0)
                            {
                                Scheduler.PostTask(() =>
                                    {
                                        this.outStream.AddReadIndex(n);
                                        if (this.outStream.Remaining > 0)
                                        {
                                            BeginWrite();
                                        }
                                    });
                            }
                            else
                            {
                                this.Close(CLOSE_SEND);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogException(e);
                            this.Close(CLOSE_SEND);

                        }
                    },
                    null);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                this.Close(CLOSE_SEND);
            }
        }
    }
}
