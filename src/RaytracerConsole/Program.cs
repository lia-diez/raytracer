using Common.Extensions;
using Common.Structures.Numerics;

Vector4 point = new Vector4(0, 0, 1, 1);
var translate = MutationMatrix.FromTranslation(1, 0, 0) * MutationMatrix.FromScale(2, 2, 2) * MutationMatrix.FromRotation(MathF.PI/2,  0, 0);
var point2 = point.Transform(translate);