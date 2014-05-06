using UnityEngine;
using System.Collections;

public class XGameWindowView : XGameView<XGameModel> {

    private bool _isMax = false;
    private bool _isMin = false;
    private int _lastWidth = 600;
    private int _lastHeight = 480;
    private float _lastX = 0;
    private float _lastY = 0;
    private GameObject _contentWrapper;

    public bool IsMax {
        get { return _isMax; }
        set { _isMax = value; }
    }
    public bool IsMin {
        get { return _isMin; }
        set { _isMin = value; }
    }

    void Start() {

    }


    void Update() {

    }

    public void Close() {
        GameObject.Destroy(gameObject);
    }

    public void Maximum(bool max, bool rePosition = true) {
        if (_isMax != max) {
            _isMax = max;
            UIWidget widget = GetComponent<UIWidget>();
            if (!_isMax) {
                widget.SetAnchor(null, 0, 0, 0, 0);
                widget.width = _lastWidth;
                widget.height = _lastHeight;
                if (rePosition) {
                    Vector3 position = transform.localPosition;
                    position.x = _lastX;
                    position.y = _lastY;
                    transform.localPosition = position;
                }
            } else {
                _lastWidth = widget.width;
                _lastHeight = widget.height;
                _lastX = transform.localPosition.x;
                _lastY = transform.localPosition.y;
                widget.SetAnchor(widget.root.gameObject, 0, 0, 0, 0);
            }
        }
    }

    public void Minimum(bool min) {
        if (_isMin != min) {
            _isMin = min;
            if (_contentWrapper == null)
                _contentWrapper = transform.Find("ContentWrapper").gameObject;
            _contentWrapper.GetComponent<UIWidget>().alpha = _isMin ? 0 : 1;
        }
    }
}
