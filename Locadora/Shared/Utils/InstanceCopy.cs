using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Shared.Utils;





public static class InstanceCopy
{
    

    /// <summary>
    /// Perform a deep Copy of the object, using Json as a serialization method. NOTE: Private members are not cloned using this method.
    /// </summary>
    /// <typeparam name="T">The type of object being copied.</typeparam>
    /// <param name="source">The object instance to copy.</param>
    /// <returns>The copied object.</returns>
    public static T DeepClone<T>(this T source)
    {
        // Don't serialize a null object, simply return the default for that object
        if (ReferenceEquals(source, null)) return default;

        // initialize inner objects individually
        // for example in default constructor some list property initialized with some values,
        // but in 'source' these items are cleaned -
        // without ObjectCreationHandling.Replace default constructor values will be added to result
        var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
    }

    public static T2 DeepClone<T, T2>(this T source)
    {
        // Don't serialize a null object, simply return the default for that object
        if (ReferenceEquals(source, null)) return default;

        // initialize inner objects individually
        // for example in default constructor some list property initialized with some values,
        // but in 'source' these items are cleaned -
        // without ObjectCreationHandling.Replace default constructor values will be added to result
        var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

        return JsonConvert.DeserializeObject<T2>(JsonConvert.SerializeObject(source), deserializeSettings);
    }


    public static void Copy(object? from, object? to)
    {
        if (from is null || to is null)
            return;



        Type fromType = from.GetType();
        Type toType = to.GetType();

        //object? dto = Activator.CreateInstance(toType);



        PropertyInfo[] properties = fromType.GetProperties();

        foreach (PropertyInfo fromProp in properties)
        {
            PropertyInfo? toProp = toType.GetProperties().FirstOrDefault(x => x.Name.ToLower() == fromProp.Name.ToLower());

            if (toProp is not null)
            {
                /*
                if(modelProp.PropertyType.IsAssignableFrom(typeof(IEnumerable)))
                {
                    Type genericEnumerableType = modelProp.PropertyType.GetGenericArguments()[0];



                }                
                else if (_mapping.ContainsKey(modelProp.PropertyType))
                {
                    Type dtoPropType = _mapping[modelProp.PropertyType];
                    object? modelValue = modelProp.GetValue(fromInstance, null);
                    object? dtoValue = MapToDto(modelValue, dtoPropType);
                    dtoProp.SetValue(dto, dtoValue);
                }
                else */





                bool isFromPropertyNullable = fromProp.PropertyType.IsGenericType && fromProp.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
                bool isToPropertyNullable = toProp.PropertyType.IsGenericType && toProp.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

                Type fromPropertyType = isFromPropertyNullable ? fromProp.PropertyType.GetGenericArguments()[0] : fromProp.PropertyType;
                Type toPropertyType = isToPropertyNullable ? toProp.PropertyType.GetGenericArguments()[0] : toProp.PropertyType;





                if (fromPropertyType == toPropertyType)
                {
                    object? modelValue = fromProp.GetValue(from, null);

                    // Evita copiar valor nulo para uma propriedade não nula:
                    if (modelValue is null && !isToPropertyNullable)
                    {
                        continue;
                    }

                    toProp.SetValue(to, modelValue);
                }
            }
        }
    }
}
