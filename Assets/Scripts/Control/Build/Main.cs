using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : BaseControl
{
    public override void DeathResponse()
    {
        GetComponent<Animation>().CrossFade("death");
    }
}
