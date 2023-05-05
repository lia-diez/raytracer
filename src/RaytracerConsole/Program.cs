using Common.Structures;
using Common.Structures.Numerics;
using Common.Structures.Traceable;

var triangle = new Triangle(new Point(1, -1, 0), new Point(1, 1, 0), new Point(0, 0, 2));
var ray = new Ray(new Point(0, 0, 0), new Vector3(1, 0, 1));
var a = triangle.FindIntersection(ray);