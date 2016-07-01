// Controller
var playerController = require('./controller/playerController');

function Player(){
  
  this.playerName = function(data){
    
    console.log("PlayerName: " + data.usr);
    
    //return data;
  }
}

module.exports = Player;