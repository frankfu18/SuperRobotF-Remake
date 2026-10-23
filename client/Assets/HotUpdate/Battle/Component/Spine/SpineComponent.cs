using Spine;
using System;
using UnityEngine;

namespace FrankGame.Battle
{
    public class SpineComponent : ComponentBase
    {
        public override ECompType ComponentType => ECompType.Spine;

        private SpineObject spineObject;
        public SpineComponent(Entity _entity) : base(_entity)
        {
        }
        
        public void SetSpine(SpineObject _spineObject)
        {
            spineObject = _spineObject;
        }

        public void SetLayer(int _layer)
        {
            if (spineObject == null) return;
            spineObject.SetLayer(_layer);
        }

        public void SetFill(float _fillNum)
        {
            spineObject.FillPhase = _fillNum;
        }

        public void PlayAction(string _actionName,bool loop=false)
        {
            if (spineObject == null) return;
            spineObject.PlayAction(_actionName,loop);
        }

        public void PlayActionSmooth(string _actionName, bool loop = false)
        {
            if (spineObject == null) return;
            spineObject.PlayActionSmooth(_actionName,loop);
        }

        public void AddAction(string _actionName, bool loop = false)
        {
            if (spineObject == null) return;
            spineObject.AddAction(_actionName, loop);
        }

        public Bone GetBone(string boneName)
        {
            if (spineObject == null)
            {
                return null;
            }
            var skeleton = spineObject.Skeleton;
            var bone = skeleton.FindBone(boneName);
            if (bone == null)
            {
                BattleLog.Error("spine骨骼未找到，name=" + boneName);
                return null;
            }

            return bone;
        }

        public Vector3 GetBonePos(string boneName)
        {
            if (spineObject == null)
            {
                return Vector3.zero;
            }
            var skeleton = spineObject.Skeleton;
            var fireBone = skeleton.FindBone(boneName);
            if (fireBone == null)
            {
                BattleLog.Error("spine骨骼未找到，name="+boneName);
                return Vector3.zero;
            }
            return spineObject.transform.TransformPoint(new Vector3(fireBone.WorldX, fireBone.WorldY, 0));
        }

        public void PlayAction(string actionName,Action<int> callback)
        {
            if (spineObject == null) return;
            spineObject.PlayAction(actionName,false,0,1, entryTrack =>
            {
                callback?.Invoke(EntityId);
            });
        }

        public string GetCurActionName()
        {
            if (spineObject == null) return null;
            return spineObject.GetCurActionName();
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (spineObject == null) return;
            spineObject.AnimationState.GetCurrent(0).TimeScale = 0;
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (spineObject == null) return;
            spineObject.AnimationState.GetCurrent(0).TimeScale = 1;
        }

        protected override void OnSpeedChange(float _speed)
        {
            base.OnSpeedChange(_speed);

            if (spineObject == null) return;
            spineObject.AnimationState.GetCurrent(0).TimeScale = _speed;
        }

        protected override string GetString()
        {
            return "curAction="+(spineObject==null?"" : spineObject.GetCurActionName());
        }

        public override void OnRecover()
        {
            
        }
    }
}