using System;
using System.Collections.Generic;

public static class WindowSlideSearch
{
    public static  int[] FindIndices<T1,T2>(T1[] window,T2[] array,Func<T1,T2,bool> matchCondition,int limit = 0)
    {
        if(window.Length == 0) return [];
        if(window.Length > array.Length) return [];

        var slideLength = array.Length - window.Length + 1;
        var limitCounter = limit > 0 ? Math.Min(limit, slideLength) : slideLength ;

        List<int> matchesIndex = [];
        for (int i = 0; i < slideLength; i++)
        {
            //Window slide
            bool matched = true;
            for (int j = 0; j < window.Length; j++)
            {
                if(!matchCondition(window[j],array[i+j]))
                {
                    matched = false;
                    break;
                }
            }

            if (matched)
            {
                matchesIndex.Add(i);

                limitCounter--;
                if(limitCounter == 0)
                    break;
            }
        }
        return [.. matchesIndex];
    }
}