using System;
using Newtonsoft.Json.Serialization;

namespace extensions_csharp.Newtonsoft
{
    /// <summary>
    /// Uses supplied mapping functions and a fallback binder to allow simple names for polymorphic '$type' field.
    /// </summary>
    public class SimplifiedSerializationBinder : ISerializationBinder
    {
        private readonly Func<Type, string?> _typeStringFn;
        private readonly Func<string, Type?> _resolveFn;
        private readonly ISerializationBinder _fallback;

        public SimplifiedSerializationBinder(Func<Type, string?> typeStringFn, Func<string, Type?> resolveFn, ISerializationBinder fallback)
        {
            _typeStringFn = typeStringFn;
            _resolveFn = resolveFn;
            _fallback = fallback;
        }

        public void BindToName(Type serializedType, out string? assemblyName, out string? typeName)
        {
            var typeString = _typeStringFn(serializedType);
            if (typeString == null)
            {
                _fallback.BindToName(serializedType, out assemblyName, out typeName);
            }
            else
            {
                typeName = typeString;
                assemblyName = null;
            }
        }

        public Type BindToType(string? assemblyName, string typeName)
        {
            return _resolveFn(typeName) ?? _fallback.BindToType(assemblyName, typeName);
        }
    }
}