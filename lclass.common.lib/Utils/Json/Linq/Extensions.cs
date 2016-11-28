﻿#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lclass.common.lib.Json.Utilities;
using System.Collections;
using System.Globalization;

namespace lclass.common.lib.Json.Linq
{
  /// <summary>
  /// Contains the LINQ to JSON extension methods.
  /// </summary>
  public static class Extensions
  {
    /// <summary>
    /// Returns a collection of tokens that contains the ancestors of every token in the source collection.
    /// </summary>
    /// <typeparam name="T">The type of the objects in source, constrained to <see cref="JToken"/>.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the source collection.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the ancestors of every node in the source collection.</returns>
    public static IJEnumerable<JToken> Ancestors<T>(this IEnumerable<T> source) where T : JToken
    {
      ValidationUtils.ArgumentNotNull(source, "source");

      return source.SelectMany(j => j.Ancestors()).AsJEnumerable();
    }

    //TODO
    //public static IEnumerable<JObject> AncestorsAndSelf<T>(this IEnumerable<T> source) where T : JObject
    //{
    //  ValidationUtils.ArgumentNotNull(source, "source");

    //  return source.SelectMany(j => j.AncestorsAndSelf());
    //}

    /// <summary>
    /// Returns a collection of tokens that contains the descendants of every token in the source collection.
    /// </summary>
    /// <typeparam name="T">The type of the objects in source, constrained to <see cref="JContainer"/>.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the source collection.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the descendants of every node in the source collection.</returns>
    public static IJEnumerable<JToken> Descendants<T>(this IEnumerable<T> source) where T : JContainer
    {
      ValidationUtils.ArgumentNotNull(source, "source");

      return source.SelectMany(j => j.Descendants()).AsJEnumerable();
    }

    //TODO
    //public static IEnumerable<JObject> DescendantsAndSelf<T>(this IEnumerable<T> source) where T : JContainer
    //{
    //  ValidationUtils.ArgumentNotNull(source, "source");

    //  return source.SelectMany(j => j.DescendantsAndSelf());
    //}

    /// <summary>
    /// Returns a collection of child properties of every object in the source collection.
    /// </summary>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="JObject"/> that contains the source collection.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="JProperty"/> that contains the properties of every object in the source collection.</returns>
    public static IJEnumerable<JProperty> Properties(this IEnumerable<JObject> source)
    {
      ValidationUtils.ArgumentNotNull(source, "source");

      return source.SelectMany(d => d.Properties()).AsJEnumerable();
    }

    /// <summary>
    /// Returns a collection of child values of every object in the source collection with the given key.
    /// </summary>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the source collection.</param>
    /// <param name="key">The token key.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the values of every node in the source collection with the given key.</returns>
    public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source, object key)
    {
      return Values<JToken, JToken>(source, key).AsJEnumerable();
    }

    /// <summary>
    /// Returns a collection of child values of every object in the source collection.
    /// </summary>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the source collection.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the values of every node in the source collection.</returns>
    public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source)
    {
      return source.Values(null);
    }

    /// <summary>
    /// Returns a collection of converted child values of every object in the source collection with the given key.
    /// </summary>
    /// <typeparam name="U">The type to convert the values to.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the source collection.</param>
    /// <param name="key">The token key.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains the converted values of every node in the source collection with the given key.</returns>
    public static IEnumerable<U> Values<U>(this IEnumerable<JToken> source, object key)
    {
      return Values<JToken, U>(source, key);
    }

    /// <summary>
    /// Returns a collection of converted child values of every object in the source collection.
    /// </summary>
    /// <typeparam name="U">The type to convert the values to.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the source collection.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains the converted values of every node in the source collection.</returns>
    public static IEnumerable<U> Values<U>(this IEnumerable<JToken> source)
    {
      return Values<JToken, U>(source, null);
    }

    /// <summary>
    /// Converts the value.
    /// </summary>
    /// <typeparam name="U">The type to convert the value to.</typeparam>
    /// <param name="value">A <see cref="JToken"/> cast as a <see cref="IEnumerable{T}"/> of <see cref="JToken"/>.</param>
    /// <returns>A converted value.</returns>
    public static U Value<U>(this IEnumerable<JToken> value)
    {
      return value.Value<JToken, U>();
    }

    /// <summary>
    /// Converts the value.
    /// </summary>
    /// <typeparam name="T">The source collection type.</typeparam>
    /// <typeparam name="U">The type to convert the value to.</typeparam>
    /// <param name="value">A <see cref="JToken"/> cast as a <see cref="IEnumerable{T}"/> of <see cref="JToken"/>.</param>
    /// <returns>A converted value.</returns>
    public static U Value<T, U>(this IEnumerable<T> value) where T : JToken
    {
      ValidationUtils.ArgumentNotNull(value, "source");

      JToken token = value as JToken;
      if (token == null)
        throw new ArgumentException("Source value must be a JToken.");

      return token.Convert<JToken, U>();
    }


    internal static IEnumerable<U> Values<T, U>(this IEnumerable<T> source, object key) where T : JToken
    {
      ValidationUtils.ArgumentNotNull(source, "source");

      foreach (JToken token in source)
      {
        if (key == null)
        {
          if (token is JValue)
          {
            yield return Convert<JValue, U>((JValue)token);
          }
          else
          {
            foreach (JToken t in token.Children())
            {
              yield return t.Convert<JToken, U>();
            }
          }
        }
        else
        {
          JToken value = token[key];
          if (value != null)
            yield return value.Convert<JToken, U>();
        }
      }

      yield break;
    }

    //TODO
    //public static IEnumerable<T> InDocumentOrder<T>(this IEnumerable<T> source) where T : JObject;

    //public static IEnumerable<JToken> Children<T>(this IEnumerable<T> source) where T : JToken
    //{
    //  ValidationUtils.ArgumentNotNull(source, "source");

    //  return source.SelectMany(c => c.Children());
    //}

    /// <summary>
    /// Returns a collection of child tokens of every array in the source collection.
    /// </summary>
    /// <typeparam name="T">The source collection type.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the source collection.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the values of every node in the source collection.</returns>
    public static IJEnumerable<JToken> Children<T>(this IEnumerable<T> source) where T : JToken
    {
      return Children<T, JToken>(source).AsJEnumerable();
    }

    /// <summary>
    /// Returns a collection of converted child tokens of every array in the source collection.
    /// </summary>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the source collection.</param>
    /// <typeparam name="U">The type to convert the values to.</typeparam>
    /// <typeparam name="T">The source collection type.</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains the converted values of every node in the source collection.</returns>
    public static IEnumerable<U> Children<T, U>(this IEnumerable<T> source) where T : JToken
    {
      ValidationUtils.ArgumentNotNull(source, "source");

      return source.SelectMany(c => c.Children()).Convert<JToken, U>();
    }

    internal static IEnumerable<U> Convert<T, U>(this IEnumerable<T> source) where T : JToken
    {
      ValidationUtils.ArgumentNotNull(source, "source");

      foreach (T token in source)
      {
        yield return Convert<JToken, U>(token);
      }
    }

    internal static U Convert<T, U>(this T token) where T : JToken
    {
      if (token == null)
        return default(U);

      if (token is U
        // don't want to cast JValue to its interfaces, want to get the internal value
        && typeof(U) != typeof(IComparable) && typeof(U) != typeof(IFormattable))
      {
        // HACK
        return (U)(object)token;
      }
      else
      {
        JValue value = token as JValue;
        if (value == null)
          throw new InvalidCastException("Cannot cast {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, token.GetType(), typeof(T)));

        if (value.Value is U)
          return (U)value.Value;

        Type targetType = typeof(U);

        if (ReflectionUtils.IsNullableType(targetType))
        {
          if (value.Value == null)
            return default(U);

          targetType = Nullable.GetUnderlyingType(targetType);
        }

        return (U)System.Convert.ChangeType(value.Value, targetType, CultureInfo.InvariantCulture);
      }
    }

    //TODO
    //public static void Remove<T>(this IEnumerable<T> source) where T : JContainer;

    /// <summary>
    /// Returns the input typed as <see cref="IJEnumerable{T}"/>.
    /// </summary>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the source collection.</param>
    /// <returns>The input typed as <see cref="IJEnumerable{T}"/>.</returns>
    public static IJEnumerable<JToken> AsJEnumerable(this IEnumerable<JToken> source)
    {
      return source.AsJEnumerable<JToken>();
    }

    /// <summary>
    /// Returns the input typed as <see cref="IJEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The source collection type.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> of <see cref="JToken"/> that contains the source collection.</param>
    /// <returns>The input typed as <see cref="IJEnumerable{T}"/>.</returns>
    public static IJEnumerable<T> AsJEnumerable<T>(this IEnumerable<T> source) where T : JToken
    {
      if (source == null)
        return null;
      else if (source is IJEnumerable<T>)
        return (IJEnumerable<T>)source;
      else
        return new JEnumerable<T>(source);
    }
  }
}