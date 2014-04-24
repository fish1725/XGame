using UnityEngine;
using System.Collections.Generic;
using ProD;

public class XGameWorldModel : XGameModel {

    public XGameWorldMapModel worldMap {
        get { return Get("worldMap") as XGameWorldMapModel; }
        set { Set("worldMap", value); }
    }

    public List<XGameCharacterModel> characters {
        get { return Get("characters") as List<XGameCharacterModel>; }
        set { Set("characters", value); }
    }

    public void AddCharacter(XGameCharacterModel c) {
        Add<XGameCharacterModel>("characters", c);
    }

    public void RemoveCharacter(XGameCharacterModel c) {
        Remove<XGameCharacterModel>("characters", c);
    }

    public XGameWorldModel() {

    }    
}
