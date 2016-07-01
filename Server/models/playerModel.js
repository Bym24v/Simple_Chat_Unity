var MongoClient = require('mongodb').MongoClient;
var assert = require('assert');
var ObjectId = require('mongodb').ObjectID;

// Config DB
var config = require('./../config');

// Player
var playerClass = require('./../player')


var test = {
  player: {
    "name": "Dev"
  }
}

var chatModelController = function(db, callback) {
  
   db.collection('chat').insertOne( test, function(err, result) {
     
    assert.equal(err, null);
    console.log("Guardado con Exito.");
     
    callback();
  });
};



var GuardarChat = {

  init: function(){
    
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