using UnityEngine;
using System.Collections;

public class XGameWindowContentItemViewString : XGameWindowContentItemView {
    protected UIInput _itemInput;

    protected override void InitInput() {
        GameObject input = XGameUIUtil.CreateInput(gameObject, Model.value.ToString());
        input.transform.localPosition = new Vector3(_leftX, 0f, 0f);
        _itemInput = input.GetComponent<UIInput>();
    }

    public override void Save() {
        Model.Save(_itemInput.value);
        //Debug.Log("key: " + Model.key + " value: " + Model.value);
    }

}
