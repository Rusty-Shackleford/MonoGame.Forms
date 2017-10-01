using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Meebey.SmartIrc4net;
using Microsoft.Xna.Framework;

namespace FormTest
{
    public class GoonClubIRC
    {
        private IrcClient irc;
        string server = "irc.dal.net";
        int port = 6667;
        string channel = "#clubgoon";
        public bool Running;
        private Thread _listenThread;

        public GoonClubIRC()
        {
            //Thread.CurrentThread.Name = "Main";
            irc = new IrcClient();
            irc.Encoding = System.Text.Encoding.UTF8;
            irc.SendDelay = 200;
            irc.ActiveChannelSyncing = true;

            irc.OnQueryMessage += new IrcEventHandler(OnQueryMessage);
            irc.OnError += new ErrorEventHandler(OnError);
            irc.OnRawMessage += new IrcEventHandler(OnRawMessage);
        }


        public void Connect()
        {
            try
            {
                irc.Connect(server, port);
            }
            catch(ConnectionException e)
            {
                Console.WriteLine("Could not connect: " + e.Message);
                return;
            }
            try
            {
                irc.Login("testuser889","testing the goonite irc client");
                irc.RfcJoin(channel);
                _listenThread = new Thread(new ThreadStart(irc.Listen));
                _listenThread.Start();

                // here we send just 3 different types of messages, 3 times for
                // testing the delay and flood protection (messagebuffer work)
                //irc.SendMessage(SendType.Message, channel, "test message (" + i.ToString() + ")");
                //irc.SendMessage(SendType.Action, channel, "thinks this is cool (" + i.ToString() + ")");
                //irc.SendMessage(SendType.Notice, channel, "SmartIrc4net rocks (" + i.ToString() + ")");
            }
            catch(ConnectionException e)
            {
                Console.WriteLine("Error at Login / Listen" + e.Message);
                return;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!_listenThread.IsAlive)
            {
                irc.Disconnect();
            }
        }


        #region [ Events ]
        private void OnQueryMessage(object sender, IrcEventArgs e)
        {
            Console.WriteLine("Received private message");
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Error: " + e.ErrorMessage);
        }

        private void OnRawMessage(object sender, IrcEventArgs e)
        {
            Console.WriteLine("Received: " + e.Data.RawMessage);
        }
        #endregion


    }
}
