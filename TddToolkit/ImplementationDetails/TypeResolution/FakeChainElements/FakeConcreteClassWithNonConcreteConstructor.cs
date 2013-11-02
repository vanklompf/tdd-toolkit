namespace TddEbook.TddToolkit.ImplementationDetails.TypeResolution.FakeChainElements
{
  internal class FakeConcreteClassWithNonConcreteConstructor<T> : IResolution<T>
  {
    readonly FallbackTypeGenerator<T> _fallbackTypeGenerator = new FallbackTypeGenerator<T>();

    public bool Applies()
    {
      return _fallbackTypeGenerator.ConstructorHasAtLeastOneNonConcreteArgumentType(typeof(T));
    }

    public T Apply()
    {
      return _fallbackTypeGenerator.GenerateInstance();
    }
  }
}