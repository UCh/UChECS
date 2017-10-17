using System.Collections.Generic;
using uchlab.ecs.processor;
using UnityEngine;
using Zenject;

namespace uchlab.ecs.zenject {
    public class ProcessorTicker : MonoBehaviour {

        [InjectOptional(Id = UpdateTick.Default)]
        private List<ITickableProcessor> tickProcessors;
        private int tickProcessorsCount;

        [InjectOptional(Id = UpdateTick.Fixed)]
        private List<ITickableProcessor> fixedTickProcessors;
        private int fixedTickProcessorsCount;

        [InjectOptional(Id = UpdateTick.Late)]
        private List<ITickableProcessor> lateTickProcessors;
        private int lateTickProcessorsCount;

        void Update() {
            tickProcessorsCount = tickProcessors.Count;
            for (var i = 0; i < tickProcessorsCount; i++) {
                tickProcessors[i].Tick();
            }
        }

        void FixedUpdate() {
            fixedTickProcessorsCount = fixedTickProcessors.Count;
            for (var i = 0; i < fixedTickProcessorsCount; i++) {
                fixedTickProcessors[i].Tick();
            }
        }

        void LateUpdate() {
            lateTickProcessorsCount = lateTickProcessors.Count;
            for (var i = 0; i < lateTickProcessorsCount; i++) {
                lateTickProcessors[i].Tick();
            }
        }


    }
}