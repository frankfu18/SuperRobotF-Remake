using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FrankGame
{
    /// <summary>
    /// 启动流程基类
    /// </summary>
    public abstract class GameLaunchProcedure
    {
        protected GameLauncher launcher;
        protected Type nextProcedure;//下一个流程

        public void Init(GameLauncher _launcher)
        {
            this.launcher = _launcher;

            OnInit();
        }

        protected virtual void OnInit()
        {

        }

        public void Enter(params object[] _params)
        {
            Log.Debug("Procedure enter: " + this.GetType().Name);

            OnEnter(_params);
        }

        protected virtual void OnEnter(params object[] _params)
        {

        }

        protected void StartCoroutine(IEnumerator routine)
        {
            this.launcher.StartCoroutine(routine);
        }

        public void Update(float _deltaTime)
        {
            OnUpdate(_deltaTime);
        }

        protected virtual void OnUpdate(float _deltaTime)
        {

        }

        protected void SetProgress(float _progress, string _text)
        {
            this.launcher.SetProgress(_progress, _text);
        }

        protected void ChangeProcedure(System.Type _procedureType, params object[] _params)
        {
            this.launcher.ExecuteProcedure(_procedureType, _params);
        }

        public void SetNextProcedure(Type _procedure)
        {
            nextProcedure = _procedure;
        }

        private void NextProcedure(params object[] _params)
        {
            if (nextProcedure != null)
            {
                ChangeProcedure(nextProcedure, _params);
            }
            else
            {
                this.launcher.CompleteLaunch();
            }
        }

        //结束所有静态流程
        protected void CompleteAllStaticProcedure()
        {
            this.launcher.ProcessCustomProcedure();
        }

        //结束当前流程
        protected void CompleteCurrentProcedure(params object[] _params)
        {
            NextProcedure(_params);
        }

        public void Exit()
        {
            Log.Debug("Procedure exit: " + this.GetType().Name);

            OnExit();
        }

        protected virtual void OnExit()
        {

        }
    }
}