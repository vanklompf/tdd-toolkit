﻿using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions;
using System.Linq;
using TddEbook.TddToolkit.ImplementationDetails.ConstraintAssertions.Reflection;
using TddEbook.TddToolkit.ImplementationDetails.CustomCollections.ConstraintAssertions;

namespace TddEbook.TddToolkit.Helpers.Constraints.EqualityOperator
{
  public class StateBasedUnEqualityMustBeImplementedInTermsOfEqualityOperator<T>
    : IConstraint where T : class
  {
    private readonly ValueObjectActivator<T> _activator;
    private int[] _indexesOfConstructorArgumentsToSkip;

    public StateBasedUnEqualityMustBeImplementedInTermsOfEqualityOperator(
      ValueObjectActivator<T> activator, int[] indexesOfConstructorArgumentsToSkip)
    {
      _activator = activator;
      this._indexesOfConstructorArgumentsToSkip = indexesOfConstructorArgumentsToSkip;
    }

    public void CheckAndRecord(ConstraintsViolations violations)
    {
      var instance1 = _activator.CreateInstanceAsValueObjectWithFreshParameters();
      for (var i = 0; i < _activator.GetConstructorParametersCount(); ++i)
      {
        if (ArgumentIsPartOfValueIdentity(i))
        {
          var instance2 = _activator.CreateInstanceAsValueObjectWithModifiedParameter(i);

          RecordedAssertions.False(Are.EqualInTermsOfEqualityOperator(instance1, instance2), "a == b should return false if both are created with different argument" + i, violations);
          RecordedAssertions.False(Are.EqualInTermsOfEqualityOperator(instance1, instance2), "b == a should return false if both are created with different argument" + i, violations);
        }
      }
    }

    private bool ArgumentIsPartOfValueIdentity(int i)
    {
      return !this._indexesOfConstructorArgumentsToSkip.Contains(i);
    }
  }
}