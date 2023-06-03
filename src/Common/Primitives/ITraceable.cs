﻿using Common.Structures;
using Common.Structures.Numerics;

namespace Common.Primitives;

public interface ITraceable
{
    public TraceResult? Trace(Ray ray);
}