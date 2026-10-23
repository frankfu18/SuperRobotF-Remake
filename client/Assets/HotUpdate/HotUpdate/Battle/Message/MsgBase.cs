using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public abstract class MsgBase
    {
        public int senderId;
        public int frame;
        public int sort;

        public MsgBase(int _senderId, int _frame, int sort)
        {
            this.senderId = _senderId;
            this.frame = _frame;
            this.sort = sort;
        }

        public void Run()
        {
            OnRun();
        }

        protected virtual void OnRun()
        {

        }
    }
}