using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class XGameWindowContentItemView : XGameView<IXGameWindowContentItemModel> {
    private UISprite _itemSprite;
    private UILabel _itemName;
    private XGameWindowModel _window;
    private List<IXGameWindowContentItemModel> _items;

    public List<IXGameWindowContentItemModel> items {
        get {
            if (_items == null) {
                _items = Model.windowContentItems;
            }
            return _items;
        }
    }
    protected float _leftX = 0f;

    public override void Init() {
        InitSprite();
        InitName();
        InitInput();
    }

    public override void InitEvents() {
        base.InitEvents();
        Model.On("change:spriteName", OnChangeSpriteName);
        Model.On("change:name", OnChangeName);
    }

    void InitSprite() {
        if (Model.spriteName != null && Model.spriteName != "") {
            GameObject imageButton = XGameUIUtil.CreateImageButton(gameObject, Model.spriteName);
            _itemSprite = imageButton.GetComponent<UISprite>();

            _leftX += _itemSprite.width + 10;
        }
    }

    void InitName() {
        GameObject nameLabel = XGameUIUtil.CreateLabel(gameObject, Model.key);
        nameLabel.transform.localPosition = new Vector3(_leftX, 0f, 0f);
        _itemName = nameLabel.GetComponent<UILabel>();
        _leftX += _itemName.width + 10;
    }

    protected virtual void InitInput() {
        GameObject textButton = XGameUIUtil.CreateTextButton(gameObject, Model.value.ToString());
        textButton.transform.localPosition = new Vector3(_leftX, 0f, 0f);
        UIButton button = textButton.GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(
                () => {
                    if (_window == null) {
                        _window = XGame.Resolve<XGameEditorController>().CreateWindow(XGame.Resolve<XGameEditorModel>());
                        XGame.Resolve<XGameWindowController>().ClearWindowContent(_window);
                        foreach (IXGameWindowContentItemModel item in items) {
                            XGame.Resolve<XGameWindowController>().AddWindowContent(_window, item);
                        }
                    }
                    XGame.Resolve<XGameWindowController>().SetWindowActive(_window, true);
                }
            ));
    }

    void OnChangeSpriteName(XGameEvent e) {
        _itemSprite.spriteName = e.data as string;
    }

    void OnChangeName(XGameEvent e) {
        _itemName.text = e.data as string;
    }

    public virtual void Save() {
        foreach (IXGameWindowContentItemModel item in items) {
            Model.Set(item.key, item.value);
            //Debug.Log("key: " + item.key + " value: " + item.value);
        }
    }

}
