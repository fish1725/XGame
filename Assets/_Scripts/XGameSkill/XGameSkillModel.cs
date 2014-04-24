using UnityEngine;
using System.Collections;

public class XGameSkillModel : XGameModel {

    public float cooldownTime {
        get { return (float)Get("cooldownTime"); }
        set { Set("cooldownTime", value); }
    }

    public float cooldownTimeR {
        get { return (float)Get("cooldownTimeR"); }
        set { Set("cooldownTimeR", value); }
    }
}
