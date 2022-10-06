using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerMaterial : MonoBehaviour
{
    [SerializeField] private MeshRenderer _body;
    [SerializeField] private MeshRenderer _goggles;

    public void SetMaterial(Material bodyMaterial, Material gogglesMaterial)
    {
        	_body.material = bodyMaterial;
        	_goggles.material = gogglesMaterial;
    }
}
