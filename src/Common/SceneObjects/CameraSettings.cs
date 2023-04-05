﻿using Common.Structures;

namespace Common.SceneObjects;

public record CameraSettings
{

    public int Fov { get; init; } = 60;
    public Point Origin { get; init; } = new (0, 0, 0);
    public Vector Direction { get; init; } = new(0, 0, 1);
    public required Rect Resolution { get; init; }
}

public record Rect(int Width, int Height);