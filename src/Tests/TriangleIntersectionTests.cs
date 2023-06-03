using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;

namespace Tests;

public class TriangleIntersectionTests
{
    public static IEnumerable<object[]> IntersectInPoint =>
        new List<object[]>
        {
            new object[]
            {
                new Triangle(new Point(0, -1, 0), new Point(0, 0, 1), new Point(0, 1, 0)),
                new Ray(new Point(1, 0, 0), (new Point(0, 0, 1)-new Point(1, 0, 0)).Normalize()),
                new Point(0, 0, 1)
            },
            new object[]
            {
                new Triangle(new Point(0, -1, 0), new Point(0, 0, 1), new Point(0, 1, 0)),
                new Ray(new Point(1, 0, 0), (new Point(0, -1, 0)-new Point(1, 0, 0)).Normalize()),
                new Point(0, -1, 0)
            },
            new object[]
            {
                new Triangle(new Point(0, -1, 0), new Point(0, 0, 1), new Point(0, 1, 0)),
                new Ray(new Point(1, 0, 0), (new Point(0, 1, 0)-new Point(1, 0, 0)).Normalize()),
                new Point(0, 1, 0)
            }
        };


    [Theory] [MemberData(nameof(IntersectInPoint))]
    public async Task TriangleIntersectRayInVertex_PointsAreEqual(Triangle triangle, Ray ray, Point point)
    {
        var intersection = triangle.Trace(ray).IntersectionPoint;
        
        Assert.Equal(point, intersection);
    }
    
    public static IEnumerable<object[]> IntersectInsideTriangle =>
        new List<object[]>
        {
            new object[]
            {
                new Triangle(new Point(1, -1, 0), new Point(1, 1, 0), new Point(0, 0, 2)),
                new Ray(new Point(0, 0, 0), (new Point(0.5f, 0, 1f)-new Point(0, 0, 0)).Normalize()),
                new Point(0.5f, 0, 1f)
            },
            new object[]
            {
                new Triangle(new Point(1, -1, 0), new Point(1, 1, 0), new Point(0, 0, 2)),
                new Ray(new Point(0, 0, 0), (new Point(0.25f, 0.25f, 0.5f)-new Point(0, 0, 0)).Normalize()),
                new Point(0.5f, 0.5f, 1f)
            },
            new object[]
            {
                new Triangle(new Point(1, -1, 0), new Point(1, 1, 0), new Point(0, 0, 2)),
                new Ray(new Point(0, 0, 0), (new Point(0.5f, -0.5f, 1f)-new Point(0, 0, 0)).Normalize()),
                new Point(0.5f, -0.5f, 1f)
            }
        };
    
    [Theory] [MemberData(nameof(IntersectInsideTriangle))]
    public async Task TriangleIntersectRayInside_PointsAreEqual(Triangle triangle, Ray ray, Point point)
    {
        var intersection = triangle.Trace(ray).IntersectionPoint;
        
        Assert.Equal(point, intersection);
    }
    
    public static IEnumerable<object[]> IntersectAtBorder =>
        new List<object[]>
        {
            new object[]
            {
                new Triangle(new Point(0, 0, 0), new Point(0, 0, 4), new Point(3, 0, 0)),
                new Ray(new Point(0, -2, 0), (new Point(0, 0, 2)-new Point(0, -2, 0)).Normalize()),
                new Point(0, 0, 2)
            },
            new object[]
            {
                new Triangle(new Point(0, 0, 0), new Point(0, 0, 4), new Point(3, 0, 0)),
                new Ray(new Point(0, -100000, 0), (new Point(2, 0, 0)-new Point(0, -100000, 0)).Normalize()),
                new Point(2, 0, 0)
            },
            new object[]
            {
                new Triangle(new Point(0, 0, 0), new Point(0, 0, 4), new Point(3, 0, 0)),
                new Ray(new Point(0, 100000, 0), (new Point(0.5f, 0, 0)-new Point(0, 100000, 0)).Normalize()),
                new Point(0.5f, 0, 0)
            }
        };
    
    [Theory] [MemberData(nameof(IntersectAtBorder))]
    public async Task TriangleIntersectAtBorder_PointsAreEqual(Triangle triangle, Ray ray, Point point)
    {
        var intersection = triangle.Trace(ray).IntersectionPoint;
        
        Assert.Equal(point, intersection);
    }

    public static IEnumerable<object[]> IntersectThroughBorder =>
        new List<object[]>
        {
            new object[]
            {
                new Triangle(new Point(0, 0, 0), new Point(0, 0, 4), new Point(3, 0, 0)),
                new Ray(new Point(-1, 0, 0), new Vector3(1, 0, 0))
            },
            new object[]
            {
                new Triangle(new Point(0, 0, 0), new Point(0, 0, 4), new Point(3, 0, 0)),
                new Ray(new Point(0, 0, 10000), new Vector3(0, 0, -1))

            },
            new object[]
            {
                new Triangle(new Point(0, 0, 0), new Point(0, 1, 0), new Point(0, 1, 1)),
                new Ray(new Point(10, 10, 0), (new Point(0, -10, -10)-new Point(0, 1, 1)).Normalize())
            }
        };
    
    [Theory] [MemberData(nameof(IntersectThroughBorder))]
    public async Task TriangleIntersectThroughBorder_NoIntersection(Triangle triangle, Ray ray)
    {
        var intersection = triangle.Trace(ray)?.IntersectionPoint;
        
        Assert.Null(intersection);
    }
    
    public static IEnumerable<object[]> RayInsideTriangle =>
        new List<object[]>
        {
            new object[]
            {
                new Triangle(new Point(-3, -2, 0), new Point(0, 5, 0), new Point(2, 0, 0)),
                new Ray(new Point(0, 3, 0), (new Point(-1, 0, 0)-new Point(0, 3, 0)).Normalize())
            },
            new object[]
            {
                new Triangle(new Point(-3, -2, 0), new Point(0, 5, 0), new Point(2, 0, 0)),
                new Ray(new Point(0, 2, 0), (new Point(0, 1, 0)-new Point(0, 2, 0)).Normalize())
            },
            new object[]
            {
                new Triangle(new Point(-3, -2, 0), new Point(0, 5, 0), new Point(2, 0, 0)),
                new Ray(new Point(0, 4, 0), (new Point(-2, 0, 0)-new Point(0, 4, 0)).Normalize())
            }
        };
    
    [Theory] [MemberData(nameof(IntersectThroughBorder))]
    public async Task RayInsideTriangle_NoIntersection(Triangle triangle, Ray ray)
    {
        var intersection = triangle.Trace(ray)?.IntersectionPoint;
        
        Assert.Null(intersection);
    }
}