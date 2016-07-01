var MongoClient = require('mongodb').MongoClient;
var assert = require('assert');
var ObjectId = require('mongodb').ObjectID;

// Puerto
var port = process.env.PORT || 8000;

// Dependencias
var io = require('socket.io')(port);

// Config DB
var config = require('./config')


var clients = [];
var currentClient;

var chatModelController = function(db, callback) {
  
   db.collection('chat').insertOne( currentClient, function(err, result) {
     
    assert.equal(err, null);
    console.log("Guardado con Exito.");
     
    callback();
  });
};

function Guardar(){
  
  MongoClient.connect(config.url, function(err, db) {
        
        //console.log('conectado a MongoDB')
      
        assert.equal(null, err);
          
        chatModelController(db, function() {
              db.close();
        });
          
      });
}

var Server = {
    
  init: function(){
    
    io.on('connection', function(socket){

      console.log("Cliente Conectado");
      
      // Login
      socket.on('login', function(data){
        
        currentClient = {
          "sockID": socket.id,
          "name": data.usr
        }
        
        for(var i = 0; i < clients.length; i++){

          if( currentClient.name != clients[i].name && currentClient.sockID != clients[i].sockID){
            Guardar();
            socket.emit('login', data);
            console.log("Usuario Guardado");
            break;
          }else{
            console.log("El usuario existe");
          }
        }
        
         clients.push(currentClient);
         
      });
      
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

module.exports = Server;