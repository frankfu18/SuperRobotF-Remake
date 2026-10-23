using System;
using System.Collections.Generic;

public class DataAndWeight<T> : IWeight
{

    public T data;
    
    public int weight;

    public int Weight => weight;
    
}