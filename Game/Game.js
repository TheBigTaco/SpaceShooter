function Game() {
  this.tickNumber = 0;
  this.isRunning = false;
  this.viewport = new Viewport();
  this.player = new Player();
}

// TODO: fix hacked-in player
Game.prototype.start = function() {
  this.isRunning = true;
  this.viewport.objects.push(this.player);
  this.main();
}

Game.prototype.main = function() {
  var currentTime = new Date().getTime();
  var prevTime = currentTime;
  var dT = 0;
  setInterval(function() {
    currentTime = new Date().getTime();
    dT += currentTime - prevTime;
    if (dT >= 10) {
      console.log("tick", dT);
      this.tickNumber++;
      dT %= 10;
    }
    prevTime = currentTime;
  }.bind(this), 5);
}

function Viewport() {
  this.objects = [];
  this.draw();
}

// Viewport.prototype.initialize = function() {
//   setInterval(this.draw.bind(this), 100);
// }

Viewport.prototype.draw = function() {
  console.log("draw");
  var canvas = document.getElementById("canvas");
  if (canvas.getContext) {
    var ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.save();

    // do the rectangle
    if(this.objects.length > 0)
    {
      ctx.fillStyle = 'rgb(200, 0, 0)';
      ctx.fillRect(this.objects[0].position.x, this.objects[0].position.y, 100, 100);
    }
    ctx.restore();
    window.requestAnimationFrame(this.draw.bind(this));
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
  game.start();

  document.onkeydown = function(event) {
    var key = event.key;
    if(key == "ArrowRight");
  };
});
