using System;
using Newtonsoft.Json.Serialization;

namespace extensions_csharp.Newtonsoft
{
    /// <summary>
    /// Keeps the '$type' field in polymorphic JSON simple.
    /// Adapted from https://stackoverflow.com/a/49287052/358006
    /// </summary>
    public class SimplifiedSerializationBinder : ISerializationBinder
    {
        private readonly Type _sampleClass;
        readonly ISerializationBinder _fallback;

        public SimplifiedSerializationBinder(Type sampleClass, ISerializationBinder fallback)
        {
            _sampleClass = sampleClass;
            _fallback = fallback ?? throw new ArgumentNullException();
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            if (serializedType.Assembly.Equals(_sampleClass.Assembly) &&
                string.Equals(serializedType.Namespace, _sampleClass.Namespace, StringComparison.Ordinal))
            {
                typeName = serializedType.Name;
                assemblyName = null!;
            }
            else
            {
                _fallback.BindToName(serializedType, out assemblyName!, out typeName!);
            }
        }

        public Type BindToType(string? assemblyName, string typeName)
        {
            return string.IsNullOrEmpty(assemblyName)
                ? Type.GetType($"{_sampleClass.Namespace}.{typeName}, {_sampleClass.Assembly.GetName()}")!
                : _fallback.BindToType(assemblyName, typeName);
        }
    }
}