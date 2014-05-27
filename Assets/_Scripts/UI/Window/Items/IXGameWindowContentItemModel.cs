using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IXGameWindowContentItemModel : IXGameModel {
    
    string spriteName { get; set; }

    string key { get; set; }

    object value { get; set; }

    Type type { get; set; }

    List<IXGameWindowContentItemModel> windowContentItems { get; set; }

    void Save(object value);
}
