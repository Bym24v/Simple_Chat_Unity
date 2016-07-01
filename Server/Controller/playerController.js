var MongoClient = require('mongodb').MongoClient;
var assert = require('assert');
var ObjectId = require('mongodb').ObjectID;

// Config DB
var config = require('./../config');

// Player
var playerClass = require('./../player')

// Model
var playerModel = require('./../models/playerModel');


var Cliente = function(db, callback) {
    
   var findPlayer = db.collection('chat').find({"player.name": playerClass["player.name"]}); //{"player.name": "Manuel"}
    
   findPlayer.each(function(err, doc) {
       
      assert.equal(err, null);
       
      if(err){

        // Guardar
        playerModel.init();

      }
     
      if (doc != null) {
        
        //if(doc.player.name != buscarPlayer.player){
          //console.log("Es diferente")
        //}
         console.dir(doc); // Output
      } else {
         callback();
      }
       
   });
};

var buscarPlayer = {
  
  init: function(){
    
    MongoClient.connect(config.url, function(err, db) {
            
      assert.equal(null, err);

      Cliente(db, function() {
        db.close();
      });
            
    });
  }
}

module.exports = buscarPlayer;