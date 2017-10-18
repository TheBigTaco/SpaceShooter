function Game() {
  this.viewport = new Viewport();
  this.player = new Player();
}

Game.prototype.Start = function() {
    this.Main();
}

// TODO: Don't hardcode player
Game.prototype.Main = function() {
  setInterval(this.viewport.Draw.bind(this), 100, this.player.position);
  //this.viewport.Draw(this.player.position);
}

function Viewport() {

}

// TODO: draw stuff other than rectangle
Viewport.prototype.Draw = function(position) {
  console.log("draw");
  var canvas = document.getElementById("canvas");
  if (canvas.getContext) {
    var ctx = canvas.getContext('2d');

    ctx.fillStyle = 'rgb(200, 0, 0)';
    ctx.fillRect(position.x, position.y, 100, 100);
  }
}

function Player() {
  this.keyDown = {
    left: false,
    right: false,
    up: false,
    down: false
  };
  this.position = new Position(50, 50);
}

function Position(x, y) {
  this.x = x;
  this.y = y;
}

$(document).ready(function() {
  var game = new Game();
  game.Start();

  document.onkeydown = function(event) {
    var key = event.key;
    if(key == "ArrowRight");
  };
});
