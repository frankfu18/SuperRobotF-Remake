using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class SpineUIComponent : ComponentBase
    {
        public override ECompType ComponentType => ECompType.SpineUI;

        public SkeletonGraphic skeletonGraphic;

        public SpineUIComponent(Entity _entity) : base(_entity)
        {
        }

        public void PlayAction(string actionName, bool loop = false, Action<TrackEntry> callback = null)
        {
            var trackEntry = skeletonGraphic.AnimationState.SetAnimation(0, actionName, loop);
            if (callback != null) trackEntry.Complete += entry => callback(entry);
        }

        public override void OnRecover()
        {
            this.skeletonGraphic = null;
        }

        protected override string GetString()
        {
            return null;
        }
    }
}