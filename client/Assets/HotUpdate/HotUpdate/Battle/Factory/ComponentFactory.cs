using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public static class ComponentFactory
    {
        public static ComponentBase Create(Entity _entity, ECompType _comType)
        {
            ComponentBase component = null;
            switch(_comType)
            {
                //公用组件
                case ECompType.Asset:
                    component = new AssetComponent(_entity);
                    break;
                case ECompType.Attribute:
                    component = new AttributeComponent(_entity);
                    break;
                
                case ECompType.BloodInt:
                    component = new BloodIntComponent(_entity);
                    break;
                
                case ECompType.Movement:
                    component = new MovementComponent(_entity);
                    break;
                case ECompType.Transform:
                    component = new TransformComponent(_entity);
                    break;
                case ECompType.Tween:
                    component = new TweenComponent(_entity);
                    break;
                case ECompType.ModelPoint:
                    component = new ModelPointComponent(_entity);
                    break;
                
                case ECompType.Spine:
                    component = new SpineComponent(_entity);
                    break;
                case ECompType.SpineUI:
                    component = new SpineUIComponent(_entity);
                    break;

                //buff触发
                case ECompType.BuffTrigger:
                    component = new BuffTriggerComponent(_entity);
                    break;
                
                //buff生效
                case ECompType.BuffEffect:
                    component = new BuffEffectComponent(_entity);
                    break;

                //特效组件
                case ECompType.Effect:
                    component = new EffectComponent(_entity);
                    break;
                //相机
                case ECompType.Camera:
                    component = new CameraComponent(_entity);
                    break;

                case ECompType.CollisionDamage:
                    component = new CollisionDamageComponent(_entity);
                    break;
                case ECompType.RandomRotate:
                    component = new RandomRotateComponent(_entity);
                    break;
                default:
                    throw new System.Exception("未找到的组件类型: " + _comType);
            }

            if (component != null)
            {
                component.Create();
            }

            return component;
        }
    }
}
