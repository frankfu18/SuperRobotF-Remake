using System;
using editor.cfg.skill;

namespace FrankGame.Battle
{
    public static class BuffDestroyFactory
    {
        public static IBuffDestroy Create(BuffDestroyType _buffDestroyType, Action _onDestoryFunc)
        {
            switch (_buffDestroyType.GetTypeId())
            {
                case ImmediateDestroy.TYPE_ID:
                    {
                        var buffDestroy = RefObjectPool<BuffDestroyImmediate>.Instance.Get();
                        buffDestroy.SetData(_buffDestroyType as ImmediateDestroy, _onDestoryFunc);

                        return buffDestroy;
                    }
                case Forever.TYPE_ID:
                    {
                        var buffDestroy = RefObjectPool<BuffDestroyForever>.Instance.Get();
                        buffDestroy.SetData(_buffDestroyType as Forever, _onDestoryFunc);

                        return buffDestroy;
                    }
                case DurationDestroy.TYPE_ID:
                    {
                        var buffDestroy = RefObjectPool<BuffDestroyDuration>.Instance.Get();
                        buffDestroy.SetData(_buffDestroyType as DurationDestroy, _onDestoryFunc);

                        return buffDestroy;
                    }
                default:
                    throw new System.Exception("未找到BuffDestroyType的类型：" + _buffDestroyType.GetType().Name);
            }
        }

        public static void Remove(BuffEffect _buffEffect, IBuffDestroy _buffDestroy)
        {
            switch (_buffEffect.BuffConfig.destroyType.GetTypeId())
            {
                case ImmediateDestroy.TYPE_ID:
                    RefObjectPool<BuffDestroyImmediate>.Instance.Recycle(_buffDestroy as BuffDestroyImmediate);
                    break;
                case Forever.TYPE_ID:
                    RefObjectPool<BuffDestroyForever>.Instance.Recycle(_buffDestroy as BuffDestroyForever);
                    break;
                case DurationDestroy.TYPE_ID:
                    RefObjectPool<BuffDestroyDuration>.Instance.Recycle(_buffDestroy as BuffDestroyDuration);
                    break;
                default:
                    throw new System.Exception("未找到BuffDestroyType的类型：" + _buffEffect.BuffConfig.destroyType.GetType().Name);
            }
        }
    }
}
