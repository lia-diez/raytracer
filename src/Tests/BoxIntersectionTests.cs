using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;
using OptimisationTree;

namespace Tests;

public class BoxIntersectionTests
{
    public static IEnumerable<object[]> Intersections =>
        new List<object[]>
        {
            new object[]
            {
                new AxisBox((-1, -2, -3), (4, 0, 4)),
                new Ray
                (
                    new Point
                        (-0.03f, -3.06f, 6.53f)
                    , new Vector3
                        (1.07f, 1.17f, -5)
                ),
                true
            },
            new object[]
            {
                new AxisBox((-1, -2, -3), (4, 0, 4)),
                new Ray
                (
                    new Point
                        (-6.64f, 1.43f, 6.55f)
                    , new Vector3
                        (3.5f, -0.89f, -1.58f)
                ),
                true
            },
            new object[]
            {                
                new AxisBox((-1, -2, -3), (4, 0, 4)),
                new Ray
                (
                    new Point
                        (0, 2.93f, 0)
                    , new Vector3
                        (-3.46f, -1.88f, 0)
                ),
                false
            },
        };

    [Theory]
    [MemberData(nameof(Intersections))]
    public async Task Intersects(AxisBox box, Ray ray, bool result)
    {
        var intersection = box.Intersects(ray);

        Assert.Equal(result, intersection);
    }
}