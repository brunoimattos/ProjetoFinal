using UnityEditor;
using UnityEngine;
 
class CustomMenu : EditorWindow
{
    [MenuItem("Custom/Add/Particles/Ellipsoid Particle Emitter (Legacy)")]
    static void AddEllipsoidParticleEmitter()
    {
       GameObject epe = new GameObject("Ellipsoid Particle Emitter");
       epe.AddComponent("EllipsoidParticleEmitter");
       epe.AddComponent<ParticleAnimator>();
       epe.AddComponent<ParticleRenderer>();
    }
}