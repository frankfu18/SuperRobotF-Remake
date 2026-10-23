using System;
using Spine;
using Spine.Unity;
using UnityEngine;

public class SpineObject : SkeletonAnimation
    {
        private MeshRenderer _meshRenderer;
        private float m_fillPhase;
        private Material m_mat;
        
        public int orderLayer;

        public float FillPhase
        {
            get { return m_fillPhase; }
            set { m_fillPhase = value; }
        }
        public override void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();

            for (int i = 0; i < _meshRenderer.materials.Length; i++)
            {
                if(_meshRenderer.materials[i].HasProperty("_FillPhase"))
                {
                    m_fillPhase = _meshRenderer.materials[i].GetFloat("_FillPhase");
                    m_mat = _meshRenderer.materials[i];
                    
                }
            }
            

            if (orderLayer > 0)
            {
                SetLayer(orderLayer);
            }

            
        }

        protected override void Update()
        {
            base.Update();
            
            if (m_fillPhase > 0)
            {
                m_fillPhase -= Time.deltaTime * 4;
                if (m_fillPhase < 0) m_fillPhase = 0;
                //_meshRenderer.material.SetFloat("_FillPhase",m_fillPhase);
                for (int i = 0; i < _meshRenderer.materials.Length; i++)
                {
                    if(_meshRenderer.materials[i].HasProperty("_FillPhase"))
                    {
                        _meshRenderer.materials[i].SetFloat("_FillPhase",m_fillPhase);
                    }
                }

                // if (m_mat != null)
                // {
                //     $"flaot".Error();
                //     m_mat.SetFloat("_FillPhase",m_fillPhase);
                // }
            }
        }

        public void SetLayer(int layer)
        {
            _meshRenderer.sortingOrder = layer;
        }
        
        public void PlayAction(string actionName, bool loop = true, int trackIndex = 0, float timeScale = 1f,
            Action<TrackEntry> callback = null)
        {
            // ClearState();
            var trackEntry = AnimationState.SetAnimation(trackIndex, actionName, loop);
            trackEntry.TimeScale = timeScale;
            if (callback != null) trackEntry.Complete += entry => callback(entry);
        }
        
        
        public void PlayActionSmooth(string actionName, bool loop = true, int trackIndex = 0, float timeScale = 1f,
            Action<TrackEntry> callback = null)
        {
            if (GetCurActionName() == actionName) return;
            var trackEntry = AnimationState.SetAnimation(trackIndex, actionName, loop);
            trackEntry.TimeScale = timeScale;
            if (callback != null) trackEntry.Complete += entry => callback(entry);
        }

        public void AddAction(string actionName,bool loop=true,int trackIndex=0)
        {
            AnimationState.AddAnimation(trackIndex, actionName, loop,0);
        }

        public string GetCurActionName(int trackIndex = 0)
        {
            if (AnimationState.GetCurrent(trackIndex) == null) 
                return "";
            return AnimationState.GetCurrent(trackIndex).Animation.Name;
        }

        public void StopCurAnim(int trackIndex = 0)
        {
            AnimationState.TimeScale = 0;
        }

        public void ResumCurAnim(int trackIndex = 0)
        {
            AnimationState.TimeScale = 1;
        }
        
        // public void TestWhite()
        // {
        //     //_meshRenderer.material.SetFloat("_TintBlack",1);
        //     //_meshRenderer.material.SetColor("_Black",Color.white);
        //     // _meshRenderer.material.DOColor(Color.white, 0.2f);
        //     AnimationState.AddAnimation(0, "idle", true,0.2f);
        // }

    }