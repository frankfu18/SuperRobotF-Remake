using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace FrankGame.Battle
{
    public static class BattleLog
    {
        private static readonly bool open_state = false;

        [Conditional(FrankGame.Log.ENABLE_LOG)]
        public static void Log(string message, params object[] _params)
        {
            LogUtil.Log(ELogType.Battle, message, _params);
        }

        [Conditional(FrankGame.Log.ENABLE_LOG)]
        public static void EntityLog(Entity entity, string message, params object[] _params)
        {
            if (!open_state)
            {
                return;
            }

            Log("(" + entity + ")" + message, _params);
        }
        
        public static void Error(string message, params object[] _params)
        {
            LogUtil.Error(ELogType.Battle, message, _params);
        }

        /// <summary>
        /// 记录 Buff 的创建、叠加、触发、执行和移除。来源或 Buff 宿主属于本地时才输出。
        /// </summary>
        [Conditional(FrankGame.Log.ENABLE_LOG)]
        public static void LocalBuff(Entity owner, int sourceEntityEid, int sourceOwnerPlayerEid,
            int runtimeBuffId, int buffId, int sourceAffixId, int triggerTargetEid, string message)
        {
            // if (!open_local_buff_state || owner == null || owner.World == null)
            // {
            //     return;
            // }
            //
            // World world = owner.World;
            // world.TryGetEntity(sourceEntityEid, out Entity source);
            // bool localSource = IsLocalPlayer(world, sourceOwnerPlayerEid) || IsLocalEntity(source);
            // bool localTarget = IsLocalEntity(owner) || IsLocalEntity(world, triggerTargetEid);
            // WriteRelated(world, "本地Buff", localSource, localTarget, false,
            //     message + " owner=" + FormatEntity(owner)
            //     + " source=" + FormatEntity(source, sourceEntityEid)
            //     + " runtimeBuffId=" + runtimeBuffId
            //     + " buffId=" + buffId
            //     + " affixId=" + sourceAffixId
            //     + " triggerTarget=" + FormatEntity(world, triggerTargetEid));
        }
    }
}
