function Game() {
  this.viewport = new Viewport();
  this.player = new Player();

  this.viewport.objects.push(this.player);
}

Game.prototype.Start = function() {
    this.Main();
}

Game.prototype.Main = function() {
  setInterval(this.viewport.Draw.bind(this.viewport), 100);
}

function Viewport() {
  this.objects = [];
}

// TODO: draw stuff other than rectangle
Viewport.prototype.Draw = function(position) {
  console.log("draw");
  var canvas = document.getElementById("canvas");
  if (canvas.getContext) {
    var ctx = canvas.getContext('2d');

    ctx.fillStyle = 'rgb(200, 0, 0)';
    ctx.fillRect(this.objects[0].position.x, this.objects[0].position.y, 100, 100);
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
