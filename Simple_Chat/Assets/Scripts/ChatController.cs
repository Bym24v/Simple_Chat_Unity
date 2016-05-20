using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using SocketIO;


public class ChatController : MonoBehaviour
{

    // 19

    // Sockets
    [Header("Socket.io")]
    public SocketIOComponent mSocket;


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

    void Awake()
    {
        mSocket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
    }

    void Start()
    {

        // Socket.io
        mSocket.On("Chat", RecivirChat);


        // Paneles
        loginPanel.SetActive(true);
        chatPanel.SetActive(false);

        // Boton Login
        btnLogin.onClick.AddListener(Login);

    }

    void FixedUpdate()
    {

        // Focus Chat
        if (inputChat.isFocused)
        {

            //Enter
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                // Si es Diferente de Bacio
                if (inputChat.text != "")
                {

                    // Enviar al Servidor 
                    EnviarChat(inputChat.text);

                    // Instatiate Text
                    GameObject i = Instantiate(isTexto);
                    i.GetComponent<Text>().text = " " + inputLogin.text + ": " + inputChat.text;
                    i.transform.SetParent(chatContent.transform);

                    // Log
                    Debug.Log(" " + inputLogin.text + ": " + inputChat.text);

                    // Reset
                    inputChat.text = "";
                    inputChat.ActivateInputField();
                    inputChat.Select();
                }

            }
        }

    }


    // Login
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


    // Enviar
    void EnviarChat(string value)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["usr"] = inputLogin.text;
        data["msg"] = inputChat.text;
        mSocket.Emit("chat", new JSONObject(data));
    }


    //Recivir
    void RecivirChat(SocketIOEvent obj)
    {

    }
}
