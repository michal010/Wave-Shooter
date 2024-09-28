using UnityEngine;
using UnityEngine.Pool;

public class VFXController : MonoBehaviour
{
    [SerializeField]
    protected ParticleSpawner _particleSpawner;

    public void HandleTargetClickedEvent(Target target)
    {
        //Spawn vfx on it.
        var vfx = _particleSpawner.SpawnParticleSystem();
        vfx.gameObject.transform.position = target.gameObject.transform.position;
        var vfxMain = vfx.main;
        vfxMain.startColor = target.AssignedColor;
    }
}
