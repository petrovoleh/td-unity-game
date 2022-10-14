<<<<<<< HEAD
using System;

namespace Unity.Burst.CompilerServices.Spmd
{
    /// <summary>
    /// Specifies that multiple calls to a method act as if they are
    /// executing in a Single Program, Multiple Data (SPMD) paradigm.
    /// </summary>
#if UNITY_BURST_EXPERIMENTAL_SPMD_ATTRIBUTE
    [AttributeUsage(AttributeTargets.Method)]
    public class SpmdAttribute : Attribute
    {
    }
#endif
}
=======
using System;

namespace Unity.Burst.CompilerServices.Spmd
{
    /// <summary>
    /// Specifies that multiple calls to a method act as if they are
    /// executing in a Single Program, Multiple Data (SPMD) paradigm.
    /// </summary>
#if UNITY_BURST_EXPERIMENTAL_SPMD_ATTRIBUTE
    [AttributeUsage(AttributeTargets.Method)]
    public class SpmdAttribute : Attribute
    {
    }
#endif
}
>>>>>>> master
