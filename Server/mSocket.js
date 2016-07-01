// Puerto
var port = process.env.PORT || 8000;

// Dependencias
var io = require('socket.io')(port);


var socket = {
  
  init: function(){
    
    io.on('connection', function(socket){
      
      console.log("Cliente Conectado");
      
      // Login
      socket.on('login', function(data){
        
        socket.emit('login', data);
        
      });
      
      // Chat
      socket.on("chat", function(data){
        
        socket.broadcast.emit('chat', data);
        
        console.log(data.msg);
        
      });
      
      socket.on("disconnect", function(){
        
        console.log("Cliente Desconectado");
        
      });
      
    })
  },

}
module.exports = socket;