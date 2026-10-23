using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    /// <summary>当前战斗唯一的相机实体，保存镜头状态与相机表现Tween。</summary>
    public class CameraEntity : Entity
    {
        public override EEntityType EntityType => EEntityType.Camera;
        public override bool RequiresUpdate => false;
        public override bool RequiresLateUpdate => false;

        public CameraComponent cameraCom;
        /// <summary>绑定主相机子节点并持有当前位置Tween，回收时负责Kill残留震动。</summary>
        public TweenComponent tweenCom;

        public CameraEntity(EntityAdmin _admin) : base(_admin)
        {
        }


        protected override void OnCreate()
        {
            this.cameraCom = AddComponent<CameraComponent>(ECompType.Camera);
            // 相机跟随由CameraComponent管理父节点；TweenComponent只绑定主相机子节点并播放局部震动。
            this.tweenCom = AddComponent<TweenComponent>(ECompType.Tween);
        }

        protected override void OnLateUpdate(float _deltaTime, float _unscaledDeltaTime)
        {
            
        }

        protected override void OnUpdate(float _deltaTime, float _unscaledDeltaTime)
        {
            
        }

        public override void OnRecover()
        {
            // 组件回收会先Kill残留Tween；快捷引用仍需清空，避免实体池复用后访问上一局组件。
            cameraCom = null;
            tweenCom = null;
        }

        protected override void OnDestroy()
        {
            
        }
    }
}
