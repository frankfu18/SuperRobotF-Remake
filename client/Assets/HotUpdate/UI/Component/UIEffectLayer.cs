using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIEffectLayer : MonoBehaviour
{
    private class ParticleLayerList
    {
        public int layer;
        public List<Renderer> renders = new List<Renderer>();
    }

    [SerializeField]
    public List<ParticleSystemRenderer> particleSystems;
    [SerializeField]
    public List<TrailRenderer> trailRenderers;

    [SerializeField]
    public List<int> recordParticleSysBaseLayerList;//记录各个粒子系统的基础层级
    [SerializeField]
    public List<int> recordTrailRenderBaseLayerList;//记录各个TrailRenderer的基础层级

    [HideInInspector]
    public int layer;

    private LODGroup lodGroup;

    public void SetLayer(int _layer)
    {
        for (int i = 0; i < particleSystems.Count; i++)
        {
            var ps = particleSystems[i];
            if(ps == null)
                Debug.LogError("特效粒子系统没找到name = " + this.name);
            ps.sortingLayerName = "UI";
            ps.sortingOrder = _layer + recordParticleSysBaseLayerList[i] - 1;
        }
        for (int i = 0; i < trailRenderers.Count; i++)
        {
            var tr = trailRenderers[i];
            if (tr == null)
                Debug.LogError("TrailRenderer没找到name = " + this.name);
            tr.sortingLayerName = "UI";
            tr.sortingOrder = _layer + recordTrailRenderBaseLayerList[i] - 1;
        }
    }

    public void SetLod(int _index)
    {
        if (lodGroup == null)
        {
            lodGroup = this.GetComponent<LODGroup>();
        }
        if (lodGroup != null)
        {
            if (_index < lodGroup.lodCount)
            {
                lodGroup.ForceLOD(_index);
            }
        }
    }

    public void SetLayerEditor()
    {
        Dictionary<int, ParticleLayerList> tempDic = new Dictionary<int, ParticleLayerList>();

        recordParticleSysBaseLayerList = new List<int>();
        particleSystems = new List<ParticleSystemRenderer>();

        var tempParticleSystems = new List<ParticleSystemRenderer>(GetComponentsInChildren<ParticleSystemRenderer>());
        foreach (var par in tempParticleSystems)
        {
            if (!par.enabled)
            {
                continue;
            }
            int order = par.sortingOrder;

            if (!tempDic.ContainsKey(order))
            {
                tempDic.Add(order, new ParticleLayerList() { layer = order });
            }

            tempDic[order].renders.Add(par);
            particleSystems.Add(par);
        }

        trailRenderers = new List<TrailRenderer>();
        var tempTrailRenderers = new List<TrailRenderer>(GetComponentsInChildren<TrailRenderer>());
        foreach (var tr in tempTrailRenderers)
        {
            if (!tr.enabled)
            {
                continue;
            }
            int order = tr.sortingOrder;

            if (!tempDic.ContainsKey(order))
            {
                tempDic.Add(order, new ParticleLayerList() { layer = order });
            }

            tempDic[order].renders.Add(tr);
            trailRenderers.Add(tr);
        }

        List<ParticleLayerList> particleLayers = new List<ParticleLayerList>(tempDic.Values);
        particleLayers.Sort((p1, p2) =>
        {
            return p1.layer - p2.layer;
        });

        layer = 0;

        for (int i = 0; i < particleLayers.Count; i++)
        {
            layer++;

            ParticleLayerList pLayer = particleLayers[i];
            foreach (var ps in pLayer.renders)
            {
                ps.sortingOrder = layer;
                ps.sortingLayerName = "UI";
            }
        }

        recordParticleSysBaseLayerList = new List<int>();
        foreach (var ps in particleSystems)
        {
            recordParticleSysBaseLayerList.Add(ps.sortingOrder);
        }

        recordTrailRenderBaseLayerList = new List<int>();
        foreach (var tr in trailRenderers)
        {
            recordTrailRenderBaseLayerList.Add(tr.sortingOrder);
        }
    }

}
