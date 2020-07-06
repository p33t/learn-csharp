using System;

namespace extensions_csharp.DependencyInjection
{
     
    public class HelloFactory
    {
        private readonly HelloWorld _world;
        private readonly HelloYellow _yellow;

        public HelloFactory(HelloWorld world, HelloYellow yellow)
        {
            _world = world;
            _yellow = yellow;
        }

        public IHello Create(HelloFlavour flavour)
        {
            switch (flavour)
            {
                case HelloFlavour.World:
                    return _world;
                case HelloFlavour.Yellow:
                    return _yellow;
                default:
                    throw new ArgumentException("Unrecognised flavour " + flavour);
            }
        }
    }
}