using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace FrankGame.Battle
{
    public class TweenComponent : ComponentBase
    {
        public override ECompType ComponentType => ECompType.Tween;

        public Transform transform;
        public Tweener posTween;
        public Tweener sizeTween;

        public TweenComponent(Entity _entity) : base(_entity)
        {
        }

        public void StopPosTween()
        {
            if (posTween != null)
            {
                posTween.Kill();
                posTween = null;
            }
        }

        public void StopSizeTween()
        {
            if (sizeTween != null)
            {
                sizeTween.Kill();
                sizeTween = null;
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (posTween != null)
            {
                posTween.Pause();
            }
            if (sizeTween != null)
            {
                sizeTween.Pause();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (posTween != null)
            {
                posTween.Play();
            }
            if (sizeTween != null)
            {
                sizeTween.Play();
            }
        }

        public override void OnRecover()
        {
            transform = null;

            StopPosTween();
            StopSizeTween();
        }

        protected override string GetString()
        {
            return $"transform:{transform}";
        }
    }
}