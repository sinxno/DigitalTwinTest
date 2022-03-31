using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class CommonUtilities
{
    public static float TestFloatFromString(string floatString)
    {
        bool correctParse = float.TryParse(floatString, NumberStyles.Float,CultureInfo.InvariantCulture ,out float value);
        if (!correctParse)
        {
            Debug.Log(floatString + "is not an float value");
            return 0f;
        }

        return value;
    }

    public static int TestIntFromString(string intString)
    {
        bool correctParse = int.TryParse(intString, out int value);
        if (!correctParse)
        {
            Debug.Log(intString + "is not an int value");
            return 0;
        }

        return value;
    }

    public static bool ConvertIntToBool(int intToConvert)
    {
        return Convert.ToBoolean(intToConvert);
    }

    public static DateTime GetTimestampAsDatetime(uint timestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
        return dateTimeOffset.DateTime;
    }

    public static uint GetDatetimeAsTimestamp(DateTime dateTime)
    {
        DateTimeOffset dateTimeOffset = dateTime;
        return (uint)dateTimeOffset.ToUnixTimeSeconds();
    }

    public static float GetAngleFromVectorFloat(Vector2 dir)
    {
        return Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI;
    }
}
