using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FrankGame
{
    public class GameLauncher
    {
        public GameEntry GameEntry
        {
            get
            {
                return gameEntry;
            }
        }

        private GameEntry gameEntry;
        private Dictionary<Type, GameLaunchProcedure> staticProcedureDic = new Dictionary<Type, GameLaunchProcedure>();//静态流程
        private List<GameLaunchProcedure> customProcedureList = new List<GameLaunchProcedure>();//自定义流程
        private Queue<GameLaunchProcedure> customProcedureQueue = new Queue<GameLaunchProcedure>();
        private GameLaunchProcedure currentProcedure;
        private LancherPanel lancherPanel;
        private Action onLaunchComplete;

        public GameLauncher(GameEntry _gameEntry, Action _onComplete)
        {
            this.gameEntry = _gameEntry;
            this.onLaunchComplete = _onComplete;

            Init();
        }

        private void Init()
        {

        }

        public void StartCoroutine(IEnumerator routine)
        {
            this.gameEntry.StartCoroutine(routine);
        }

        public void LoadPanel(System.Action _onLoadStartFunc = null, System.Action<float> _onLoadFinishFunc = null)
        {
            lancherPanel = LancherPanel.Load(gameEntry.UICanvas.gameObject, _onLoadStartFunc, _onLoadFinishFunc);
        }

        public void Update(float _deltaTime)
        {

        }

        public void RegisterProcedure(Type _procedureType)
        {
            staticProcedureDic[_procedureType] = CreateGameProcedure(_procedureType);
        }

        public void AddCustomProcedure(Type _procedureType)
        {
            var procedure = CreateGameProcedure(_procedureType);

            staticProcedureDic[_procedureType] = procedure;

            if (customProcedureList.Count > 0)
            {
                customProcedureList[customProcedureList.Count - 1].SetNextProcedure(_procedureType);
            }

            customProcedureList.Add(procedure);
            customProcedureQueue.Enqueue(procedure);
        }

        public void ProcessCustomProcedure()
        {
            if (customProcedureQueue.Count > 0)
            {
                ExecuteProcedure(customProcedureQueue.Dequeue());
            }
            else
            {
                CompleteLaunch();
            }
        }

        public void CompleteLaunch()
        {
            onLaunchComplete?.Invoke();
            lancherPanel?.Close();
            Log.Debug("Procedure complete.");
        }

        private GameLaunchProcedure CreateGameProcedure(Type _procedureType)
        {
            GameLaunchProcedure procedure = (GameLaunchProcedure)Activator.CreateInstance(_procedureType);
            procedure.Init(this);

            return procedure;
        }

        public void SetProgress(float _progress, string _text)
        {
            lancherPanel.SetProgress(_progress, _text);
        }

        public void ExecuteProcedure(Type _procedureType, params object[] _params)
        {
            if (currentProcedure != null)
            {
                currentProcedure.Exit();
            }

            if (staticProcedureDic.TryGetValue(_procedureType, out currentProcedure))
            {
                currentProcedure.Enter(_params);
            }
            else
            {
                Log.Error("Procedure not exist. " + _procedureType.Name);
            }
        }

        public void ExecuteProcedure(GameLaunchProcedure _procedure, params object[] _params)
        {
            if (currentProcedure != null)
            {
                currentProcedure.Exit();
            }
            currentProcedure = _procedure;
            currentProcedure.Enter(_params);
        }


        public LancherPanel GetLancherPanel()
        {
            return lancherPanel;
        }
    }
}