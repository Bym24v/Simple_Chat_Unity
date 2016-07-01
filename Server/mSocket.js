// Puerto
var port = process.env.PORT || 8000;

// Dependencias
var io = require('socket.io')(port);

// Model
var playerController = require('./controller/playerController');
var playerModel = require('./models/playerModel');



//chatController.init();

//chatDb.init();

var socket = {
  
  init: function(){
    
    io.on('connection', function(socket){
      
      // Player
      var player = require('./player');
      playerInst = new player();
      
      console.log("Cliente Conectado");
      
      // Login
      socket.on('login', playerInst.playerName);
      
      // Chat
      socket.on("chat", function(data){
        
        socket.broadcast.emit('chat', data);
        console.log(data.msg);
        
      });
      
      socket.on("disconnect", function(){
        
        console.log("Cliente Desconectado");
        
      });
      
    });
  }

}
module.exports = socket;