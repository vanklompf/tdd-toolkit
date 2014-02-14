﻿using System;
namespace TddEbook.TypeReflection
{
  public interface IFieldWrapper
  {
    bool IsNotDeveloperDefinedReadOnlyField();
    string ShouldNotBeMutableButIs();
  }
}