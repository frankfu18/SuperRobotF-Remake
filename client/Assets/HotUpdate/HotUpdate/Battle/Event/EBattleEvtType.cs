
public enum EBattleEvtType
{
    EntityHpDamage,//实体血量伤害
    EntityHpHeal,//实体血量治疗
    EntityAttributeChange,//属性变化
    EntityHpIntDamage,//实体血量伤害
    EntityHpIntHeal,//实体血量治疗

    AttackMiss,//攻击未命中

    // 星噬模式内部事件。
    PlanetResourceChanged,// planetEid, stageResource, fusedResource
    EvolutionOptionsChanged,// planetEid, candidateVersion, candidateAffixIds
    PlanetStageChanged,// planetEid, oldStage, newStage
    StoneAbsorbed,// stoneEid, planetEid, resourceValue
    StoneBroken,// stoneEid, attackerPlayerEid, dropStoneId, dropCount
    PlanetDefeated,// planetEid, ownerPlayerEid, killerPlayerEid
    SatelliteStateChanged,// satelliteEid, active, respawnRemainSecond
}
