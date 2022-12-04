using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Data;
using Runtime.Grid;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Runtime.Blocks.LightBlocks
{
    public abstract class BaseLightBlock : BaseBlock
    {
        [SerializeField] private bool canEmit;
        [SerializeField] private Directions emitDirections;
        [SerializeField] private bool canReceive;
        [SerializeField] private Directions receiveDirections;
        [SerializeField] private int maxLightDistance = 50;

        public UnityEvent<Vector3Int> stopEmittingTo = new();
        public UnityEvent<LightEmitData> onEmitLight = new();
        public UnityEvent onFirstLightReceived = new();
        public UnityEvent onLastLightLost = new();


        protected readonly Dictionary<Vector3Int, BaseLightBlock> ReceivingFrom = new();
        protected readonly Dictionary<Vector3Int, BaseLightBlock> EmittingTo = new();

        protected virtual LightReceiveData ReceiveLight(LightEmitData emitData)
        {
            LightReceiveData receiveData = new LightReceiveData();
            if (canReceive == false) return receiveData;

            if (!ReceivingFrom.ContainsKey(emitData.Direction))
            {
                ReceivingFrom[emitData.Direction] = emitData.Emitter;
                if (ReceivingFrom.Keys.Count == 1) onFirstLightReceived?.Invoke();
            }

            return receiveData;
        }

        protected virtual void StopReceiveLight(LightLoseData emitData)
        {
            StartCoroutine(DelayedStopReceive(emitData));
        }

        private IEnumerator DelayedStopReceive(LightLoseData emitData)
        {
            yield return null;
            if (canReceive == false) yield break;
            if (ReceivingFrom.ContainsKey(emitData.Direction)) ReceivingFrom.Remove(emitData.Direction);

            if (ReceivingFrom.Keys.Count == 0)
            {
                onLastLightLost?.Invoke();
            }
        }

        protected virtual LightEmitData EmitLight(Vector3Int direction)
        {
            LightEmitData data = new LightEmitData(direction, this,
                transform.position.ToVector3Int() + (direction * maxLightDistance));

            if (!canEmit) return data;

            Vector3Int startPos = transform.position.ToVector3Int() + direction;
            BaseBlock hittedBlock =
                GridManager.Instance.GetFirstBlockInDirection(startPos, direction, maxLightDistance);

            if (hittedBlock == null) return data;
            data.HitBlock = hittedBlock;
            data.HitPosition = hittedBlock.transform.position.ToVector3Int();
            if (!hittedBlock.GetType().IsSubclassOf(typeof(BaseLightBlock))) return data;

            ((BaseLightBlock) data.HitBlock).ReceiveLight(data);

            return data;
        }

        protected virtual bool CanEmitTo(Directions directions)
        {
            if (directions == Directions.Nothing) return false;
            return emitDirections.HasFlag(directions);
        }

        protected virtual void Update()
        {
            List<BaseLightBlock> foundBlocks = new List<BaseLightBlock>();
            foreach (Directions value in Enum.GetValues(typeof(Directions)))
            {
                if (!CanEmitTo(value)) continue;
                LightEmitData emitData = EmitLight(value.ToVector3Int());

                onEmitLight?.Invoke(emitData);

                if (!emitData.HitLightBlock) continue;
                BaseLightBlock lightBlock = emitData.HitBlock as BaseLightBlock;
                foundBlocks.Add(lightBlock);
                if (!EmittingTo.ContainsKey(value.ToVector3Int())) EmittingTo.Add(value.ToVector3Int(), lightBlock);
            }

            CheckStopEmitting(foundBlocks);
        }

        /// <summary>
        /// Stops emitting to all blocks not in the list
        /// </summary>
        /// <param name="blocks"></param>
        private void CheckStopEmitting(List<BaseLightBlock> blocks)
        {
            List<Vector3Int> toRemove = new();
            foreach (var emittingToKey in EmittingTo.Keys)
            {
                if (blocks.Contains(EmittingTo[emittingToKey])) continue;
                EmittingTo[emittingToKey].StopReceiveLight(new LightLoseData(emittingToKey, EmittingTo[emittingToKey]));
                toRemove.Add(emittingToKey);
            }

            toRemove.ForEach(i => EmittingTo.Remove(i));
        }

        #region Public getters

        public bool CanEmit => canEmit;

        public bool CanReceive => canReceive;

        public Directions ReceiveDirections => receiveDirections;

        public Directions EmitDirections => emitDirections;

        public int MAXLightDistance => maxLightDistance;

        #endregion
    }
}