using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class XGameWindowContentItemView : XGameView<IXGameWindowContentItemModel> {
    private UISprite _itemSprite;
    private UILabel _itemName;
    private UIInput _itemInput;
    private XGameWindowModel _window;
    private List<IXGameWindowContentItemModel> _items;
    private float _leftX = 0f;
    private static Type[] editableTypes = { typeof(string), typeof(int), typeof(float) };

    public override void Init() {
        _items = Model.windowContentItems;
        InitSprite();
        InitName();
        InitInput();
    }

    public override void InitEvents() {
        base.InitEvents();
        Model.On("change:spriteName", OnChangeSpriteName);
        Model.On("change:name", OnChangeName);
    }

    public bool editable {
        get { return Array.Exists<Type>(editableTypes, t => t == Model.value.GetType()); }
    }

    void InitSprite() {
        if (Model.spriteName != null && Model.spriteName != "") {
            GameObject imageButton = XGameUIUtil.CreateImageButton(gameObject, Model.spriteName);
            _itemSprite = imageButton.GetComponent<UISprite>();
            UIButton button = imageButton.GetComponent<UIButton>();
            button.onClick.Add(new EventDelegate(
                    () => {
                        if (_window == null) {
                            _window = XGame.Resolve<XGameEditorController>().CreateWindow(XGame.Resolve<XGameEditorModel>());
                        }
                        XGame.Resolve<XGameWindowController>().SetWindowActive(_window, true);
                        XGame.Resolve<XGameWindowController>().ClearWindowContent(_window);
                        foreach (IXGameWindowContentItemModel item in _items) {
                            XGame.Resolve<XGameWindowController>().AddWindowContent(_window, item);
                        }
                    }
                ));
            _leftX += _itemSprite.width + 10;
        }
    }

    void InitName() {
        GameObject nameLabel = XGameUIUtil.CreateLabel(gameObject, Model.name);
        nameLabel.transform.localPosition = new Vector3(_leftX, 0f, 0f);
        _itemName = nameLabel.GetComponent<UILabel>();
        _leftX += _itemName.width + 10;
    }

    void InitInput() {
        if (editable) {
            GameObject input = XGameUIUtil.CreateInput(gameObject, Model.value.ToString());
            input.transform.localPosition = new Vector3(_leftX, 0f, 0f);
            _itemInput = input.GetComponent<UIInput>();
        }
    }

    void OnChangeSpriteName(XGameEvent e) {
        _itemSprite.spriteName = e.data as string;
    }

    void OnChangeName(XGameEvent e) {
        _itemName.text = e.data as string;
    }

    public void Save() {
        if (_itemInput != null) {
            Model.Save(_itemInput.value);
        }
    }

}
