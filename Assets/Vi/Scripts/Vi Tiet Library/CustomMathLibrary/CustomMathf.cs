using UnityEngine;
using CustomMathLibrary.Interpolation.Easing;

namespace CustomMathLibrary
{
    public static class CustomMathf
    {
        public enum LerpType
        {
            Linear,
            Quadratic,
            Cubic,
            Quartic,
            Quintic,
            Sinusoidal,
            Exponential,
            Circular,
            Elastic,
            Back,
            Bounce
        }

        public static float CalculateLerpValue(float lerpValue, LerpType easingType, bool isZeroToOne)
        {
            switch (easingType)
            {
                case LerpType.Linear:
                    lerpValue = Linear.InOut(lerpValue);
                    break;
                case LerpType.Quadratic:
                    lerpValue = Quadratic.InOut(lerpValue);
                    break;
                case LerpType.Cubic:
                    lerpValue = Cubic.InOut(lerpValue);
                    break;
                case LerpType.Quartic:
                    lerpValue = Quartic.InOut(lerpValue);
                    break;
                case LerpType.Quintic:
                    lerpValue = Quintic.InOut(lerpValue);
                    break;
                case LerpType.Sinusoidal:
                    lerpValue = Sinusoidal.InOut(lerpValue);
                    break;
                case LerpType.Exponential:
                    lerpValue = Exponential.InOut(lerpValue);
                    break;
                case LerpType.Circular:
                    lerpValue = Circular.InOut(lerpValue);
                    break;
                case LerpType.Elastic:
                    lerpValue = Elastic.InOut(lerpValue);
                    break;
                case LerpType.Back:
                    lerpValue = Back.InOut(lerpValue);
                    break;
                case LerpType.Bounce:
                    lerpValue = Bounce.InOut(lerpValue);
                    break;
                default:
                    return -1f;
            }

            return lerpValue;
        }

        public static float CalculateLerpValueClamp01(float lerpValue, LerpType easingType, bool isZeroToOne)
        {
            switch (easingType)
            {
                case LerpType.Linear:
                    lerpValue = Linear.InOut(lerpValue);
                    break;
                case LerpType.Quadratic:
                    lerpValue = Quadratic.InOut(lerpValue);
                    break;
                case LerpType.Cubic:
                    lerpValue = Cubic.InOut(lerpValue);
                    break;
                case LerpType.Quartic:
                    lerpValue = Quartic.InOut(lerpValue);
                    break;
                case LerpType.Quintic:
                    lerpValue = Quintic.InOut(lerpValue);
                    break;
                case LerpType.Sinusoidal:
                    lerpValue = Sinusoidal.InOut(lerpValue);
                    break;
                case LerpType.Exponential:
                    lerpValue = Exponential.InOut(lerpValue);
                    break;
                case LerpType.Circular:
                    lerpValue = Circular.InOut(lerpValue);
                    break;
                case LerpType.Elastic:
                    lerpValue = Elastic.InOut(lerpValue);
                    break;
                case LerpType.Back:
                    lerpValue = Back.InOut(lerpValue);
                    break;
                case LerpType.Bounce:
                    lerpValue = Bounce.InOut(lerpValue);
                    break;
                default:
                    return -1f;
            }

            lerpValue = ClampMinMax(0f, 1f, lerpValue);

            return lerpValue;
        }

        public static float ClampMinMax(float min, float max, float value)
        {
            if (value > max) value = max;
            else if (value < min) value = min;

            return value;
        }
    }
}