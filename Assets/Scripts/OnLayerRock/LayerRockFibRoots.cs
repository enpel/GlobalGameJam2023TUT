using UnityEngine;

public class LayerRockFibRoots : LayerRock
{
    [SerializeField] FibrousRootsManager m_fib;

    // Update is called once per frame
    public override void AbsorbedObject()
    {
        m_fib.InstantiateFabirousRoots();
        Invoke("EnableObj", _disableTime);
    }
}
