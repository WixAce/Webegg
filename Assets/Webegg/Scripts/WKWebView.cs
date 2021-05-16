using UnityEngine;
using UnityEngine.iOS;

public class WKWebView : MonoBehaviour {
	WebViewObject _webViewObject;

	[SerializeField] private string _url=string.Empty;

	private string _idfa, _idfv;

	void Start() {
		Init();
		InitUserProperty();
	}

	private void InitUserProperty() {
		Application.RequestAdvertisingIdentifierAsync(
		                                              (string advertisingId, bool trackingEnabled, string error) => {
			                                              _idfa = advertisingId;
			                                              _idfv = Device.vendorIdentifier;
			                                              _webViewObject.LoadURL(string.Format(_url, _idfa, _idfv));
			                                              _webViewObject.SetVisibility(true);
		                                              }
		                                             );
	}

	void Init() {
		_webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
		_webViewObject.Init(
		                    cb: (msg) => { Debug.Log(string.Format("CallFromJS[{0}]", msg)); },
		                    err: (msg) => { Debug.Log(string.Format("CallOnError[{0}]", msg)); },
		                    started: (msg) => { Debug.Log(string.Format("CallOnStarted[{0}]", msg)); },
		                    ld: (msg) => { _webViewObject.SetVisibility(true); }, enableWKWebView: true,
		                    transparent: true);
	}
}