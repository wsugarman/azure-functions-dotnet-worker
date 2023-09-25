// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.Azure.Functions.Worker.Converters
{
    internal sealed class ParseConverter : IInputConverter
    {
        public ValueTask<ConversionResult> ConvertAsync(ConverterContext context)
        {
            if (context.TargetType.IsValueType && context.Source is string value && !string.IsNullOrWhiteSpace(value))
            {
                Type targetType = context.TargetType;
                if (targetType == typeof(bool) || targetType == typeof(bool?))
                {
                    if (bool.TryParse(value, out bool result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(byte) || targetType == typeof(byte?))
                {
                    if (byte.TryParse(value, out byte result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(sbyte) || targetType == typeof(sbyte?))
                {
                    if (sbyte.TryParse(value, out sbyte result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(ushort) || targetType == typeof(ushort?))
                {
                    if (ushort.TryParse(value, out ushort result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(short) || targetType == typeof(short?))
                {
                    if (short.TryParse(value, out short result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(uint) || targetType == typeof(uint?))
                {
                    if (uint.TryParse(value, out uint result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(int) || targetType == typeof(int?))
                {
                    if (int.TryParse(value, out int result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(ulong) || targetType == typeof(ulong?))
                {
                    if (ulong.TryParse(value, out ulong result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(long) || targetType == typeof(long?))
                {
                    if (long.TryParse(value, out long result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(float) || targetType == typeof(float?))
                {
                    if (float.TryParse(value, out float result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(double) || targetType == typeof(double?))
                {
                    if (double.TryParse(value, out double result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(decimal) || targetType == typeof(decimal?))
                {
                    if (decimal.TryParse(value, out decimal result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
                {
                    if (DateTime.TryParse(value, out DateTime result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(DateTimeOffset) || targetType == typeof(DateTimeOffset?))
                {
                    if (DateTimeOffset.TryParse(value, out DateTimeOffset result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(TimeSpan) || targetType == typeof(TimeSpan?))
                {
                    if (TimeSpan.TryParse(value, out TimeSpan result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(Guid) || targetType == typeof(Guid?))
                {
                    if (Guid.TryParse(value, out Guid result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
                else if (targetType == typeof(char) || targetType == typeof(char?))
                {
                    if (char.TryParse(value, out char result))
                        return new ValueTask<ConversionResult>(ConversionResult.Success(result));
                }
            }

            return new ValueTask<ConversionResult>(ConversionResult.Unhandled());
        }
    }
}
