var MongoClient = require('mongodb').MongoClient;
var assert = require('assert');
var ObjectId = require('mongodb').ObjectID;

// Config DB
var config = require('./../config');

var test = {
  "name": "Manuel" 
}
    
var chatModelController = function(db, callback) {
  
   db.collection('chat').insertOne( GuardarChat.init(), function(err, result) {
     
    assert.equal(err, null);
    console.log("Guardado con Exito.");
     
    callback();
  });
};

var GuardarChat = {
  
  init: function(data){
    console.log(data)
    return data;
  },
  
  Start: function(){
    
    MongoClient.connect(config.url, function(err, db) {
        
        //console.log('conectado a MongoDB')
      
        assert.equal(null, err);
          
        chatModelController(db, function() {
              db.close();
        });
          
      });
  }
}

module.exports = GuardarChat;