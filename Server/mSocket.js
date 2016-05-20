// Puerto
var port = process.env.PORT || 8000;

// Dependencias
var io = require('socket.io')(port);


var socket = {
  
  init: function(){
    
    io.on('connection', function(socket){
      
      console.log("Cliente Conectado");
      
      socket.on("chat", function(data){
        
        console.log(data);
        
      });
      
      socket.on("disconnect", function(){
        
        console.log("Cliente Desconectado");
        
      });
      
    })
  },

}
module.exports = socket;