using System.Globalization;
using System.Text.RegularExpressions;
using Cms.Shared.Shared.Models;

namespace Cms.Shared.Shared.Utils;

public static class Extensions
{
    public static string ToPascalCase(this string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length < 2)
            return value;
        return string.Concat(value[..1].ToUpper(), value.AsSpan(1));
    }

    public static string MergeTimeSlots(IList<string> timeSlots)
    {
        if (timeSlots.Count == 0)
        {
            return "";
        }
            
        int GetMinutesFromTime(string time)
        {
            var timeParts = time.Split(':');
            var hours = int.Parse(timeParts[0]);
            var minutes = int.Parse(timeParts[1]);
            return hours * 60 + minutes;
        }

        var mergedIntervals = new List<string> { timeSlots[0] };

        for (var i = 1; i < timeSlots.Count; i++)
        {
            var currentInterval = timeSlots[i];
            var prevInterval = mergedIntervals.Last().Split('-');
            var currentIntervalArray = currentInterval.Split('-');

            var prevEndMinutes = GetMinutesFromTime(prevInterval[1]);
            var currentStartMinutes = GetMinutesFromTime(currentIntervalArray[0]);

            if (currentStartMinutes <= prevEndMinutes)
            {
                mergedIntervals[^1] = $"{prevInterval[0]}-{currentIntervalArray[1]}";
            }
            else
            {
                mergedIntervals.Add(currentInterval);
            }
        }

        return string.Join(", ", mergedIntervals);
    }

    public static DateTimeOffset TryParseDateTimeOffset(this string input)
    {
        try
        {
            var culture = CultureInfo.CreateSpecificCulture("ru-RU");
            var style = DateTimeStyles.None;
            var format = "dd.MM.yyyy HH:mm:ss";
            var success = DateTimeOffset.TryParseExact(input + " 00:00:00", format, culture, style, out var result);
            if (!success)
            {
                throw new Exception("Ошибка при парсинга");
            }

            return new DateTimeOffset(result.Year, result.Month, result.Day, 0, 0, 0, TimeSpan.Zero);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public static DateTimeOffset TryParseDateTimeOffset(this Filter filter)
    {
        return filter.Value.GetString()!.TryParseDateTimeOffset();
    }
    
    public static object? GetDeepPropertyValue(this object? source, string path)
    {
        var pp = path.Split('.');
        if (source == null) return source;
        var t = source.GetType();
        foreach (var prop in pp)
        {
            var propInfo = t.GetProperty(prop);
            if (propInfo == null)
                throw new ArgumentException($"Properties path is not correct: {path}");
            
            source = propInfo.GetValue(source, null);
            t = propInfo.PropertyType;
        }

        return source;
    }

    public static string ReplaceTemplate<Type>(this string str, Type model)
    {
        var result = str;
        var regex = new Regex(@"(?<!\{)\{([a-zA-Z]+).*?\}(?!})");
        var matches = regex.Matches(str);
        if (matches.Count == 0) return result;
        foreach (Match match in matches)
        {
            var template = match.ToString();
            var fieldName = template.Substring(1, template.Length - 2);
            var fieldValue = model.GetDeepPropertyValue(fieldName);
            if(fieldValue == null)
            {
                fieldValue = "";
            }
            fieldValue = fieldValue.GetType().Name != nameof(String) ? fieldValue.ToString() : fieldValue;
            result = result.Replace(template, (string)fieldValue);
        }

        return result;
    }

    public static bool IsBase64String(this string base64)
    {
        var buffer = new Span<byte>(new byte[base64.Length]);
        return Convert.TryFromBase64String(base64, buffer, out var bytesParsed);
    }
    
    public static string ClearPhoneNumber(this string phoneNumber)
    {
        var formattedNumber = phoneNumber.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");

        if (phoneNumber.StartsWith("+"))
        {
            formattedNumber = "+" + formattedNumber;
        }

        return formattedNumber;
    }
    
    
}