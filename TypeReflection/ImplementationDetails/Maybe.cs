﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TddEbook.TypeReflection.ImplementationDetails
{
  public static class Maybe
  {
    public static Maybe<T> Wrap<T>(T instance) where T : class
    {
      return new Maybe<T>(instance);
    }
  }

  public struct Maybe<T> where T : class
  {
    private readonly T _value;
    private static readonly Maybe<T> _nothing = new Maybe<T>();

    public Maybe(T instance)
      : this()
    {
      if (instance != null)
      {
        HasValue = true;
        _value = instance;
      }
    }

    public bool HasValue { get; private set; }

    public T Value
    {
      get
      {
        if (HasValue)
        {
          return _value;
        }
        else
        {
          throw new Exception("No instance of type " + typeof(T));
        }
      }
    }

    public static Maybe<T> Nothing
    {
      get { return _nothing; }
    }

    public T ValueOr(T alternativeValue)
    {
      return HasValue ? Value : alternativeValue;
    }

    public override string ToString()
    {
      return HasValue ? Value.ToString() : "<Nothing>";
    }
  }


}