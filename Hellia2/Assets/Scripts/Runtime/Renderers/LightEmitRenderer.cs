using System;
using System.Collections.Generic;
using Runtime.Blocks.LightBlocks;
using Runtime.Data;
using UnityEngine;
using Utilities;

namespace Runtime.Renderers
{
    [RequireComponent(typeof(BaseLightBlock))]
    public class LightEmitRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRendererPrefab;

        private BaseLightBlock _baseLightBlock;
        private readonly Dictionary<Vector3Int, LineRenderer> _lineRenderers = new();

        private void Awake()
        {
            _baseLightBlock = GetComponent<BaseLightBlock>();
            _baseLightBlock.onEmitLight?.AddListener(Render);
            _baseLightBlock.onLastLightLost?.AddListener(StopRenderers);
            SpawnRenderers();
        }

        private void SpawnRenderers()
        {
            foreach (Directions value in Enum.GetValues(typeof(Directions)))
            {
                if (value == Directions.Nothing) continue;
                if (_baseLightBlock.EmitDirections.HasFlag(value))
                {
                    LineRenderer lineRenderer = Instantiate(lineRendererPrefab, transform);
                    _lineRenderers[value.ToVector3Int()] = lineRenderer;
                }
            }
        }

        private void OnDestroy()
        {
            _baseLightBlock.onEmitLight?.RemoveListener(Render);
            _baseLightBlock.onLastLightLost?.RemoveListener(StopRenderers);
        }

        private void Render(LightEmitData lightEmitData)
        {
            LineRenderer lineRenderer = _lineRenderers[lightEmitData.Direction];
            lineRenderer.SetPosition(0, transform.localPosition);
            lineRenderer.SetPosition(1, lightEmitData.HitPosition);
        }

        private void StopRenderers()
        {
            foreach (var keyValuePair in _lineRenderers)
            {
                keyValuePair.Value.SetPositions(new [] {Vector3.zero, Vector3.zero});
            }
        }
    }
}