using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ChatController : MonoBehaviour {


    // GameObject 
    [Header("Paneles")]
    public GameObject loginPanel;
    public GameObject chatPanel;


    // Login
    [Header("Login Input")]
    public InputField inputLogin;
    public Button btnLogin;

    // Chat
    [Header("Chat Input")]
    public InputField inputChat;
    


    void Start () {

        loginPanel.SetActive(true);
        chatPanel.SetActive(false);

        // Login
        btnLogin.onClick.AddListener(Login);

	}


    void Login()
    {
        if (inputLogin.text != "")
        {
            loginPanel.SetActive(false);
            chatPanel.SetActive(true);
        }
    }
	
	
	void FixedUpdate () {
	


	}
}
