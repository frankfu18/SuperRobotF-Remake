namespace FrankGame.Battle
{
    /// <summary>战斗场景实体固定根资源 ID；具体模型资源仍由各自玩法配置提供。</summary>
    public static class BattleSceneAssetConst
    {
        /// <summary>星球真实物理根，承载 PlanetSceneItem、Rigidbody 与 Collider。</summary>
        public const string PlanetRootAssetId = "planet_item";

        /// <summary>伴星和分裂伴星身体的表现根，承载 SatelliteSceneItem。</summary>
        public const string SatelliteRootAssetId = "satellite_item";

        /// <summary>碎石与小型陨石的表现根，承载 StoneSceneItem。</summary>
        public const string StoneRootAssetId = "stone_item";
    }
}
