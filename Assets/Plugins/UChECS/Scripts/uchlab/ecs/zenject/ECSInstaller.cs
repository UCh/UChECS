using System;
using UnityEngine;
using Zenject;

namespace uchlab.ecs.zenject {
    public class ECSInstaller : MonoInstaller {

        public override void InstallBindings() {
            Container.Bind<IEntityRepository>().To<EntityRepository>().AsSingle();
            BindDeltaTimeProviders();
        }

        private void BindDeltaTimeProviders() {
            BindDeltaProvider(() => Time.deltaTime, DeltaSecondProvider.Default);
            BindDeltaProvider(() => Time.fixedDeltaTime, DeltaSecondProvider.Fixed);
            BindDeltaProvider(() => Time.smoothDeltaTime, DeltaSecondProvider.Smooth);
            BindDeltaProvider(() => Time.unscaledDeltaTime, DeltaSecondProvider.Unscaled);
        }

        private void BindDeltaProvider(Func<float> callback, DeltaSecondProvider id) {
            Container.BindInstance<Func<float>>(callback).WithId(id);
        }
    }
}