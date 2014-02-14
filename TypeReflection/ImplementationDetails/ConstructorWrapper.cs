using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TddEbook.TypeReflection.ImplementationDetails
{
  internal class ConstructorWrapper : IConstructorWrapper
  {
    private readonly ConstructorInfo _constructor;

    public ConstructorWrapper(ConstructorInfo constructor)
    {
      _constructor = constructor;
    }

    public bool HasNonPointerArgumentsOnly()
    {
      var parameters = _constructor.GetParameters();
      if(!parameters.Any(p => p.ParameterType.IsPointer))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool HasLessParametersThan(int numberOfParams)
    {
      var parameters = _constructor.GetParameters();
      if (parameters.Count() < numberOfParams)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public int GetParametersCount()
    {
      return _constructor.GetParameters().Count();
    }

    public bool HasAbstractOrInterfaceArguments()
    {
      foreach (var argument in _constructor.GetParameters())
      {
        if (argument.ParameterType.IsAbstract || argument.ParameterType.IsInterface)
        {
          return true;
        }
      }
      return false;
    }

    public static IEnumerable<IConstructorWrapper> ExtractAllFrom(Type type)
    {
      var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                 .Select(c => new ConstructorWrapper(c)).ToList();
      
      if (!constructors.Any())
      {
        return new List<IConstructorWrapper> {new DefaultParameterlessConstructor()};
      }
      else
      {
        return constructors;
      }
    }

    public List<object> GenerateAnyParameterValues(Func<Type, object> instanceGenerator)
    {
      var constructorParams = _constructor.GetParameters();
      var constructorValues = new List<object>();

      foreach (var constructorParam in constructorParams)
      {
        constructorValues.Add(instanceGenerator(constructorParam.ParameterType));
      }
      return constructorValues;
    }

    public bool IsParameterless()
    {
      return this.GetParametersCount() == 0;
    }
  }
}