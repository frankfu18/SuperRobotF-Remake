using System.Collections.Generic;

public class LoadSpriteCache : Singlet<LoadSpriteCache> {
   
    private readonly Dictionary<string ,Dictionary<string, int>> SpriteIndexMapping = new Dictionary<string, Dictionary<string, int>>();

    private readonly Dictionary<string, UnityEngine.Sprite[]> totalSpriteArrayDic = new Dictionary<string, UnityEngine.Sprite[]>();

    /// <summary>
    /// 尝试获取
    /// </summary>
    /// <param name="_textName">图片名称</param>
    /// <param name="_atlasName">图集</param>
    /// <returns>是否获取到</returns>
    public bool TryGet(string _textName, string _atlasName, out UnityEngine.Sprite _sprite) {
        _sprite = null;
        // 先从缓存中获取
        if (SpriteIndexMapping.TryGetValue(_atlasName, out var list)) {
            if (list.TryGetValue(_textName, out var index)) {
                _sprite = totalSpriteArrayDic[_atlasName][index];
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 尝试获取图片列表
    /// </summary>
    /// <param name="_atlasName">图集名称</param>
    /// <param name="_sprites">图集结果</param>
    /// <returns></returns>
    public bool TryGets(string _atlasName, out UnityEngine.Sprite[] _sprites) {
        // 先从缓存中获取
        if (totalSpriteArrayDic.TryGetValue(_atlasName, out _sprites)) {
            return true;
        } else {
            _sprites = null;
        }
        return false;
    }
    /// <summary>
    /// 新增一个图集，不能重复
    /// </summary>
    /// <param name="_atlasName"></param>
    /// <param name="_sprites"></param>
    /// <exception cref="System.Exception"></exception>
    public UnityEngine.Sprite[] AddAndGets(string _atlasName, UnityEngine.Object[] _sprites) {
        if (_sprites == null || _sprites.Length == 0) {
            throw new System.Exception($"[{_atlasName}] 图集不能为空");
        }
        var spriteIndexes = new Dictionary<string, int>();
        var canUsedSprite = new List<UnityEngine.Sprite>() ;
        // 获取可用的图片
        foreach (var sp in _sprites) {
            if (!(sp is UnityEngine.Sprite)) {
                continue;
            }
            canUsedSprite.Add(sp as UnityEngine.Sprite);
        }
        // 是否存在
        if (canUsedSprite.Count == 0) {
            throw new System.Exception($"[{_atlasName}] 图集不能为空");
        }
        var spriteArray = canUsedSprite.ToArray();
        for (var i = 0;i < spriteArray.Length;i ++) {
            spriteIndexes.Add(spriteArray[i].name, i);
        }
        SpriteIndexMapping.Add(_atlasName, spriteIndexes);
        totalSpriteArrayDic.Add(_atlasName, spriteArray);
        return spriteArray;
    }



    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="_atlasName"></param>
    /// <returns></returns>
    public bool Remove(string _atlasName) {
        if (SpriteIndexMapping.ContainsKey(_atlasName)) {
            SpriteIndexMapping.Remove(_atlasName);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 设置，可以重复设置
    /// </summary>
    /// <param name="_atlasName"></param>
    /// <param name="_sprites"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public UnityEngine.Sprite[] Set(string _atlasName, UnityEngine.Object[] _sprites) {
        if (_sprites == null || _sprites.Length == 0) {
            throw new System.Exception($"[{_atlasName}] 图集不能为空");
        }
        UnityEngine.Sprite[] originArray = null;
        if (totalSpriteArrayDic.TryGetValue(_atlasName, out originArray)) {
            // 删除之前的
            Remove(_atlasName);
        }
        // 新增
        AddAndGets(_atlasName, _sprites);
        return originArray;
    }

    /// <summary>
    /// 设置并设置图集缓存
    /// </summary>
    /// <param name="_textName">图片名称</param>
    /// <param name="_atlasName">图集名称</param>
    /// <param name="_sprites">设置</param>
    /// <returns>结果</returns>
    /// <exception cref="System.Exception"></exception>
    public UnityEngine.Sprite AddAndGet(string _textName, string _atlasName, UnityEngine.Object[] _sprites) {
        // 检查是否存在，如果存在，则异常一下
        if (SpriteIndexMapping.TryGetValue(_atlasName, out var _)) {
            throw new System.Exception($"[{_atlasName}] 图集已经加载，请不要重新加载[{_textName}]。");
        }
        // 设置
        if (_sprites == null || _sprites.Length == 0) {
            throw new System.Exception($"[{_atlasName}] 图集不能为空。");
        }
        // 直接新增
        AddAndGets(_atlasName, _sprites);
        // 检查是否存在
        if (SpriteIndexMapping[_atlasName].TryGetValue(_textName, out var index)) {
            return totalSpriteArrayDic[_atlasName][index];
        }
        throw new System.Exception($"[{_atlasName}] 图集中不存在[{_textName}]图片资源。");
    }

}
