using UnityEngine;
using System.Collections;

public class XGameWindowContentItemView : XGameView<IXGameWindowContentItemModel> {
    private UISprite _itemSprite;
    private UILabel _itemName;
    private XGameWindowModel _window;

    public override void Init() {
        InitSprite();
        InitName();
    }

    public override void InitEvents() {
        base.InitEvents();
        Model.On("change:spriteName", OnChangeSpriteName);
        Model.On("change:name", OnChangeName);
    }

    void InitSprite() {
        GameObject imageButton = XGameUIUtil.CreateImageButton(gameObject, Model.spriteName);
        _itemSprite = imageButton.GetComponent<UISprite>();
        UIButton button = imageButton.GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(
                () => {
                    if (_window == null) {
                        _window = XGame.Resolve<XGameEditorController>().CreateWindow(XGame.Resolve<XGameEditorModel>());
                    }
                    XGame.Resolve<XGameWindowController>().SetWindowActive(_window, true);
                }
            ));
    }

    void InitName() {
        GameObject nameLabel = XGameUIUtil.CreateLabel(gameObject, Model.name);
        nameLabel.transform.localPosition = new Vector3(120f, 0f, 0f);
        _itemName = nameLabel.GetComponent<UILabel>();
    }

    void OnChangeSpriteName(XGameEvent e) {
        _itemSprite.spriteName = e.data as string;
    }

    void OnChangeName(XGameEvent e) {
        _itemName.text = e.data as string;
    }

}
