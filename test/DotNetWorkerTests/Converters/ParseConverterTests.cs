// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Converters;
using Xunit;

namespace Microsoft.Azure.Functions.Worker.Tests.Converters
{
    public class ParseConverterTests
    {
        private readonly ParseConverter _converter = new();

        [Fact]
        public async Task ConversionSkippedForInvalidTargetType()
        {
            TestConverterContext context = new(typeof(string), "source");
            ConversionResult result = await _converter.ConvertAsync(context);

            Assert.Equal(ConversionStatus.Unhandled, result.Status);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task ConversionSkippedForInvalidSourceType()
        {
            TestConverterContext context = new(typeof(int), 123);
            ConversionResult result = await _converter.ConvertAsync(context);

            Assert.Equal(ConversionStatus.Unhandled, result.Status);
            Assert.Null(result.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t\r\n  ")]
        public async Task ConversionSkippedForInvalidSourceValue(string source)
        {
            TestConverterContext context = new(typeof(int), source);
            ConversionResult result = await _converter.ConvertAsync(context);

            Assert.Equal(ConversionStatus.Unhandled, result.Status);
            Assert.Null(result.Value);
        }

        [Theory]
        [InlineData("true")]
        [InlineData("FALSE")]
        [InlineData("TrUe")]
        public Task ConversionSuccessfulForValidSourceBoolean(string source)
        {
            return AssertSuccessfulConversionAsync(source, bool.Parse(source));
        }

        [Theory]
        [InlineData("1")]
        [InlineData("0")]
        [InlineData("truue")]
        public Task ConversionFailedForInvalidSourceBoolean(string source)
        {
            return AssertFailedConversionAsync<bool>(source);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("42")]
        [InlineData("0100")]
        [InlineData("255")]
        public Task ConversionSuccessfulForValidSourceByte(string source)
        {
            return AssertSuccessfulConversionAsync(source, byte.Parse(source));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("256")]
        [InlineData("hello")]
        public Task ConversionFailedForInvalidSourceByte(string source)
        {
            return AssertFailedConversionAsync<byte>(source);
        }

        [Theory]
        [InlineData("-128")]
        [InlineData("-3")]
        [InlineData("000")]
        [InlineData("101")]
        [InlineData("127")]
        public Task ConversionSuccessfulForValidSourceSByte(string source)
        {
            return AssertSuccessfulConversionAsync(source, sbyte.Parse(source));
        }

        [Theory]
        [InlineData("-129")]
        [InlineData("128")]
        [InlineData("world")]
        public Task ConversionFailedForInvalidSourceSByte(string source)
        {
            return AssertFailedConversionAsync<sbyte>(source);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("123")]
        [InlineData("05678")]
        [InlineData("65535")]
        public Task ConversionSuccessfulForValidSourceUShort(string source)
        {
            return AssertSuccessfulConversionAsync(source, ushort.Parse(source));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("65536")]
        [InlineData("true")]
        public Task ConversionFailedForInvalidSourceUShort(string source)
        {
            return AssertFailedConversionAsync<ushort>(source);
        }

        [Theory]
        [InlineData("-32768")]
        [InlineData("-642")]
        [InlineData("0")]
        [InlineData("00975")]
        [InlineData("32767")]
        public Task ConversionSuccessfulForValidSourceShort(string source)
        {
            return AssertSuccessfulConversionAsync(source, short.Parse(source));
        }

        [Theory]
        [InlineData("-32769")]
        [InlineData("32768")]
        [InlineData("f04ca9b7-7279-401f-9224-ce4e4117c69a")]
        public Task ConversionFailedForInvalidSourceShort(string source)
        {
            return AssertFailedConversionAsync<short>(source);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("0007770")]
        [InlineData("8467583")]
        [InlineData("4294967295")]
        public Task ConversionSuccessfulForValidSourceUInt(string source)
        {
            return AssertSuccessfulConversionAsync(source, uint.Parse(source));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("4294967296")]
        [InlineData("!")]
        public Task ConversionFailedForInvalidSourceUInt(string source)
        {
            return AssertFailedConversionAsync<uint>(source);
        }

        [Theory]
        [InlineData("-2147483648")]
        [InlineData("-010101010")]
        [InlineData("0")]
        [InlineData("214748")]
        [InlineData("2147483647")]
        public Task ConversionSuccessfulForValidSourceInt(string source)
        {
            return AssertSuccessfulConversionAsync(source, int.Parse(source));
        }

        [Theory]
        [InlineData("-2147483649")]
        [InlineData("2147483648")]
        [InlineData("00:01:30")]
        public Task ConversionFailedForInvalidSourceInt(string source)
        {
            return AssertFailedConversionAsync<int>(source);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("4407370")]
        [InlineData("00709551615000")]
        [InlineData("18446744073709551615")]
        public Task ConversionSuccessfulForValidSourceULong(string source)
        {
            return AssertSuccessfulConversionAsync(source, ulong.Parse(source));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("18446744073709551616")]
        [InlineData("1.23")]
        public Task ConversionFailedForInvalidSourceULong(string source)
        {
            return AssertFailedConversionAsync<ulong>(source);
        }

        [Theory]
        [InlineData("-9223372036854775808")]
        [InlineData("-3685477")]
        [InlineData("0")]
        [InlineData("00054775008022330")]
        [InlineData("9223372036854775807")]
        public Task ConversionSuccessfulForValidSourceLong(string source)
        {
            return AssertSuccessfulConversionAsync(source, long.Parse(source));
        }

        [Theory]
        [InlineData("-9223372036854775809")]
        [InlineData("9223372036854775808")]
        [InlineData("-0113.4")]
        public Task ConversionFailedForInvalidSourceLong(string source)
        {
            return AssertFailedConversionAsync<long>(source);
        }

        [Theory]
        [InlineData("-∞")]
        [InlineData("-3.402823E+39")]
        [InlineData("-3.402823E+38")]
        [InlineData("-0134")]
        [InlineData("0")]
        [InlineData("8716.010101E-2")]
        [InlineData("3.402823e+38")]
        [InlineData("3.402823e+39")]
        [InlineData("∞")]
        public Task ConversionSuccessfulForValidSourceFloat(string source)
        {
            return AssertSuccessfulConversionAsync(source, float.Parse(source));
        }

        [Theory]
        [InlineData("3054caf4-64e6-4157-9b2e-c41bd128c98c")]
        [InlineData("foo bar baz")]
        public Task ConversionFailedForInvalidSourceFloat(string source)
        {
            return AssertFailedConversionAsync<float>(source);
        }

        [Theory]
        [InlineData("-∞")]
        [InlineData("-1.7976931348623157E+309")]
        [InlineData("-1.7976931348623157e+308")]
        [InlineData("-797693.1348E-4")]
        [InlineData("00")]
        [InlineData("03134")]
        [InlineData("1.7976931348623157E+308")]
        [InlineData("1.7976931348623157E+309")]
        [InlineData("∞")]
        public Task ConversionSuccessfulForValidSourceDouble(string source)
        {
            return AssertSuccessfulConversionAsync(source, double.Parse(source));
        }

        [Theory]
        [InlineData("T")]
        [InlineData("9/24/2023 11:45:58 PM")]
        public Task ConversionFailedForInvalidSourceDouble(string source)
        {
            return AssertFailedConversionAsync<double>(source);
        }

        [Theory]
        [InlineData("-79228162514264337593543950335")]
        [InlineData("-25,162.1378")]
        [InlineData("0.0")]
        [InlineData("1.62345")]
        [InlineData("79228162514264337593543950335")]
        public Task ConversionSuccessfulForValidSourceDecimal(string source)
        {
            return AssertSuccessfulConversionAsync(source, decimal.Parse(source));
        }

        [Theory]
        [InlineData("-79228162514264337593543950336")]
        [InlineData("79228162514264337593543950336")]
        [InlineData("false")]
        public Task ConversionFailedForInvalidSourceDecimal(string source)
        {
            return AssertFailedConversionAsync<decimal>(source);
        }

        [Theory]
        [InlineData("1/1/0001 12:00:00 AM")]
        [InlineData("04/11/2022")]
        [InlineData("04-13-2022")]
        [InlineData("2022-08-15")]
        [InlineData("2022-04-11T17:17:12.9326256Z")]
        [InlineData("9999-12-31T23:59:59.9999999")]
        public Task ConversionSuccessfulForValidSourceDateTime(string source)
        {
            return AssertSuccessfulConversionAsync(source, DateTime.Parse(source));
        }

        [Theory]
        [InlineData("12/31/0000 11:59:59 PM")]
        [InlineData("10000-01-01T00:00:00")]
        [InlineData("1.02:03:04")]
        public Task ConversionFailedForInvalidSourceDateTime(string source)
        {
            return AssertFailedConversionAsync<DateTime>(source);
        }

        [Theory]
        [InlineData("Monday, January 1, 0001 12:00:00 AM GMT", 0)]
        [InlineData("2022-05-16T08:16:53.1880572-03:00", -3)]
        [InlineData("2022-05-16T08:17:54.1880573-01:00", -1)]
        [InlineData("2022-05-16T08:16:53", null)]
        [InlineData("2022-05-16", null)]
        [InlineData("Fri, 31 Dec 9999 23:59:59 GMT", 0)]
        public Task ConversionSuccessfulForValidSourceDateTimeOffset(string source, int? expectedOffsetHours)
        {
            return AssertSuccessfulConversionAsync(
                source,
                DateTimeOffset.Parse(source),
                (s, t, actual) =>
                {
                    // when no offset info is present in input value, offset of local timezone will be set as the offset of the DateTimeOffSet instance.
                    var expectedOffSetHours = expectedOffsetHours ?? TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).Hours;
                    Assert.Equal(expectedOffSetHours, actual.Offset.Hours);
                });
        }

        [Theory]
        [InlineData("Monday, January 1, 0001 12:00:00 AM +08:00")]
        [InlineData("Sat, 01 Dec 10000 00:00:00 GMT")]
        [InlineData("1.02:03:04")]
        public Task ConversionFailedForInvalidSourceDateTimeOffset(string source)
        {
            return AssertFailedConversionAsync<DateTimeOffset>(source);
        }

        [Theory]
        [InlineData("-10675199.02:48:05.4775808")]
        [InlineData("-00:25:30.5000000")]
        [InlineData("0")]
        [InlineData("12:34:56.789")]
        [InlineData("10675199.02:48:05.4775807")]
        public Task ConversionSuccessfulForValidSourceTimeSpan(string source)
        {
            return AssertSuccessfulConversionAsync(source, TimeSpan.Parse(source));
        }

        [Theory]
        [InlineData("-10675199.02:48:05.4775809")]
        [InlineData("10675199.02:48:05.4775808")]
        [InlineData("1/1/0001 12:00:00 AM")]
        public Task ConversionFailedForInvalidSourceTimeSpan(string source)
        {
            return AssertFailedConversionAsync<TimeSpan>(source);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData("6cf8151848244ca78a169e14b4f13beb")]
        [InlineData("6cf81518-4824-4ca7-8a16-9e14b4f13beb")]
        [InlineData("{6cf81518-4824-4ca7-8a16-9e14b4f13beb}")]
        [InlineData("(6cf81518-4824-4ca7-8a16-9e14b4f13beb)")]
        public Task ConversionSuccessfulForValidSourceGuid(string source)
        {
            return AssertSuccessfulConversionAsync(source, Guid.Parse(source));
        }

        [Theory]
        [InlineData("a-string-with-four-hyphens")]
        [InlineData("12345")]
        [InlineData("true")]
        [InlineData("6cf81518-4824-4ca7-8a16")]
        [InlineData("6cf81518-4824-4ca7-8a16_9e14b4f13beb")]
        [InlineData("ValidGuidInsideAString6cf81518-4824-4ca7-8a16-9e14b4f13beb")]
        public Task ConversionFailedForInvalidSourceGuid(string source)
        {
            return AssertFailedConversionAsync<Guid>(source);
        }

        [Theory]
        [InlineData("\0")]
        [InlineData("A")]
        [InlineData("Ӓ")]
        [InlineData("\uffff")]
        public Task ConversionSuccessfulForValidSourceChar(string source)
        {
            return AssertSuccessfulConversionAsync(source, char.Parse(source));
        }

        [Theory]
        [InlineData("hello world")]
        [InlineData("10")]
        public Task ConversionFailedForInvalidSourceChar(string source)
        {
            return AssertFailedConversionAsync<char>(source);
        }

        private async Task AssertSuccessfulConversionAsync<T>(string source, T expected, Action<string, Type, T> additionalAssertion = null)
            where T : struct
        {
            foreach (Type parameterType in new[] { typeof(T), typeof(Nullable<>).MakeGenericType(typeof(T)) })
            {
                TestConverterContext context = new(parameterType, source);
                ConversionResult result = await _converter.ConvertAsync(context);

                Assert.Equal(ConversionStatus.Succeeded, result.Status);
                T actual = Assert.IsType<T>(result.Value);
                Assert.Equal(expected, actual);

                additionalAssertion?.Invoke(source, parameterType, actual);
            }
        }

        private async Task AssertFailedConversionAsync<T>(string source)
            where T : struct
        {
            foreach (Type parameterType in new[] { typeof(T), typeof(Nullable<>).MakeGenericType(typeof(T)) })
            {
                TestConverterContext context = new(parameterType, source);
                ConversionResult result = await _converter.ConvertAsync(context);

                Assert.Equal(ConversionStatus.Unhandled, result.Status);
                Assert.Null(result.Value);
            }
        }
    }
}
