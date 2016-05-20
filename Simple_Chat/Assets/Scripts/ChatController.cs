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

    // Chat Content
    [Header("Chat Input")]
    public GameObject chatContent;

    // Texto
    [Header("Texto Spawn")]
    public GameObject isTexto;
    


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
        else
        {
            inputLogin.text = "Dev";
        }
    }
	
	
	void FixedUpdate () {


        if (inputChat.isFocused)
        {                                      //OR
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log(" " + inputLogin.text + ": " + inputChat.text);


                if (inputChat.text != "")
                {
                    // Instatiate Text
                    GameObject i = Instantiate(isTexto);
                    i.GetComponent<Text>().text = " " + inputLogin.text + ": " + inputChat.text;
                    i.transform.SetParent(chatContent.transform);


                    // Reset
                    inputChat.text = "";
                    inputChat.ActivateInputField();
                    inputChat.Select();
                }
               
            }
        }
	}
}
