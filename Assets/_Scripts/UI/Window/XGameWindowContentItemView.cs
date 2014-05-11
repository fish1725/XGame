using UnityEngine;
using System.Collections;

public class XGameWindowContentItemView : XGameView<XGameWindowContentItemModel> {
    private UISprite _itemSprite;
    private UILabel _itemName;

    public override void Init() {
        _itemSprite = NGUITools.AddSprite(gameObject, XGame.Resolve<UIAtlas>(), Model.spriteName);
        _itemName = NGUITools.AddChild<UILabel>(gameObject);
        _itemName.trueTypeFont = XGame.Resolve<Font>();
        _itemName.text = Model.name;
        _itemName.fontSize = 32;
        _itemName.color = Color.black;
        _itemName.overflowMethod = UILabel.Overflow.ResizeFreely;
        _itemName.effectStyle = UILabel.Effect.Shadow;
        _itemName.effectColor = Color.white;
        _itemName.leftAnchor.target = _itemSprite.transform;
        _itemName.leftAnchor.relative = 1;
        _itemName.leftAnchor.absolute = 20;
        NGUITools.AddWidgetCollider(_itemSprite.gameObject);
    }

    public override void InitEvents() {
        base.InitEvents();
        Model.On("change:spriteName", OnChangeSpriteName);
        Model.On("change:name", OnChangeName);
    }

    void OnChangeSpriteName(XGameEvent e) {
        _itemSprite.spriteName = e.data as string;
    }

    void OnChangeName(XGameEvent e) {
        _itemName.text = e.data as string;
    }

}
