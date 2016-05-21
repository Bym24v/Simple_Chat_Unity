using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using SocketIO;
using System.Text.RegularExpressions;

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
    public GameObject player;

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
        mSocket.On("chat", RecivirChat);
        mSocket.On("login", OnLogin);


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

                    // Instatiate Text
                    GameObject i = Instantiate(isTexto);
                    i.GetComponent<Text>().text = " " + inputLogin.text + ": " + inputChat.text;
                    i.transform.SetParent(chatContent.transform);

                    // Enviar al Servidor 
                    EnviarChat(inputChat.text);

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
            var data = new Dictionary<string, string>();
            data["usr"] = inputLogin.text;
            mSocket.Emit("login", new JSONObject(data));
            Debug.Log("login");
            
        }
        else
        {
            inputLogin.text = "Dev";
        }
    }

    void OnLogin(SocketIOEvent obj)
    {
        //GameObject setPlayer =  (GameObject)Instantiate(player, new Vector3(50, 1, 50), Quaternion.identity);
        //setPlayer.gameObject.name = JsonString(obj.data.GetField("usr").ToString(), "\"");
        loginPanel.SetActive(false);
        chatPanel.SetActive(true);
    }

    // Parse
    string JsonString(string target, string s)
    {
        string[] newString = Regex.Split(target, s);

        return newString[1];
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
        GameObject SetText = (GameObject)Instantiate(isTexto);
        SetText.GetComponent<Text>().text = " " + JsonString(obj.data.GetField("usr").ToString(), "\"") + ": " + JsonString(obj.data.GetField("msg").ToString(), "\"");
        SetText.transform.SetParent(chatContent.transform);
    }
}
