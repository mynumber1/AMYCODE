using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lclass.common.lib.Json
{
  /// <summary>
  /// Instructs the <see cref="JsonSerializer"/> to use the specified constructor when deserializing that object.
  /// </summary>
  [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
  public sealed class JsonConstructorAttribute : Attribute
  {
  }
}