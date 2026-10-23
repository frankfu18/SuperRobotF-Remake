using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FrankGame.Battle
{
    public class MessageManager
    {
        private List<MsgBase> msgList = new List<MsgBase>();
        private int curFrame;
        private Queue<MsgBase> processMsgQueue = new Queue<MsgBase>();

        public void AddMsg(MsgBase _msg)
        {
            this.msgList.Add(_msg);
        }

        public void Run()
        {
            this.curFrame = 0;

            this.msgList.Sort((a, b) => 
            { 
                if(a.frame == b.frame)
                {
                    return a.sort.CompareTo(b.sort);
                }

                return a.frame.CompareTo(b.frame);
            });

            this.processMsgQueue.Clear();
            foreach (var msg in this.msgList)
            {
                this.processMsgQueue.Enqueue(msg);
            }
            this.msgList.Clear();
        }

        public void Update(int _deltaFrame)
        {
            while (this.processMsgQueue.Count > 0)
            {
                if(this.processMsgQueue.Peek().frame > this.curFrame)
                {
                    break;
                }

                MsgBase msg = this.processMsgQueue.Dequeue();
                msg.Run();
            }

            this.curFrame += _deltaFrame;
        }
    }
}
