using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace FrankGame.Battle
{
    public class TransformComponent : ComponentBase, ICompClone<TransformComponent>
    {
        public override ECompType ComponentType => ECompType.Transform;

        public Vector2 position;
        public Quaternion quaternion = Quaternion.identity;
        public Vector3 scale = Vector3.one;
        
        public Vector2 direction;
        public Vector2 lastPos;
        public Vector2 lastDir;

        private Tweener posTween;
        private Vector2 tweenCachePos;
        
        public TransformComponent(Entity _entity) : base(_entity)
        {

        }

        public void SetPos(Vector2 _pos, bool _init = false, bool _tween = false)
        {
            if (_init)
            {
                this.lastPos = _pos;
                this.position = _pos;
            }
            else
            {
                if (_tween)
                {
                    if (_pos == this.tweenCachePos)
                    {
                        return;
                    }
                }
                else
                {
                    if (_pos == this.position)
                    {
                        return;
                    }
                }

                if (posTween != null)
                {
                    posTween.Kill(false);
                    posTween = null;

                    if (!_tween)
                    {
                        this.position = this.tweenCachePos;
                    }
                }

                this.lastPos = this.position;

                if (_tween)
                {
                    this.tweenCachePos = _pos;

                    posTween = DOTween.To(() => this.lastPos, x =>
                    {
                        this.position = x;
                    }, this.tweenCachePos, 0.1f);
                }
                else
                {
                    this.position = _pos;
                    this.tweenCachePos = this.position;
                }
            }

        }

        public void SetDir(Vector2 _dir, bool _init = false)
        {
            if (_init)
            {
                this.lastDir = _dir;
            }
            else
            {
                this.lastDir = this.direction;
            }

            this.direction = _dir;
        }

        public void Reset()
        {
            position = Vector2.zero;
            quaternion = Quaternion.identity;
            scale = Vector3.one;
        }

        protected override string GetString()
        {
            return $"position:({position.x}, {position.y}), direction:({direction.x}, {direction.y}), lastPos:({lastPos.x}, {lastPos.y}), lastDir:({lastDir.x}, {lastDir.y})";
        }

        public void Clone(TransformComponent _sourceComp)
        {
            this.position = _sourceComp.position;
            this.direction = _sourceComp.direction;
            this.lastPos = _sourceComp.lastPos;
            this.lastDir = _sourceComp.lastDir;
            this.quaternion = _sourceComp.quaternion;
            this.scale = _sourceComp.scale;
        }

        public override void OnRecover()
        {
            this.position = Vector3.zero;
            this.direction = Vector3.zero;
            this.lastPos = Vector3.zero;
            this.lastDir = Vector3.zero;
            this.quaternion = Quaternion.identity;
            this.scale = Vector3.one;
            if (posTween != null)
            {
                posTween.Kill(false);
                posTween = null;
            }
        }
    }
}
